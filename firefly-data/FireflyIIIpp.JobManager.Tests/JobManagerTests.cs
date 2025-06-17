using Xunit;
using Firefly_iii_pp_Runner.Services;
using FireflyIIIpp.Core.Tests.Utilities;
using FireflyIIIpp.Tests.Fakes;
using FireflyIIIpp.NodeRed.Abstractions;
using Firefly_pp_Runner.Models.Runner.Dtos;
using System.Collections.Generic;
using FireflyIIIpp.Core.Models;
using FireflyIIIpp.FireflyIII.Abstractions.Models.Dtos;
using System;
using FireflyIIIpp.FireflyIII.Abstractions.Models;
using Newtonsoft.Json;
using Firefly_pp_Runner.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Firefly_pp_Runner.Models.Runner;

namespace FireflyIIIpp.Tests
{
    public class JobManagerTests
    {
        private readonly JobManager _sut;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly FakeFireflyIIIService _fireflyIIIService;
        private readonly FakeNodeRedService _nodeRedService;

        public JobManagerTests()
        {
            _fireflyIIIService = new FakeFireflyIIIService
            {
                PageSize = 5
            };
            _nodeRedService = new FakeNodeRedService();
            _sut = new JobManager(
                new FakeLogger<JobManager>(),
                _fireflyIIIService,
                _nodeRedService);
            _serializerSettings = new JsonSerializerSettings().ConfigureFireflyppRunnerSettings();
        }

        [Fact]
        public async Task ShouldNotSkipTransactionsWhenQueryResultsChange()
        {
            _fireflyIIIService.Transactions = new Dictionary<string, TransactionDto>();
            for(int i=1; i<11; i++)
            {
                var id = Guid.NewGuid().ToString();
                _fireflyIIIService.Transactions[id] = new TransactionDto
                {
                    Id = id,
                    Attributes = new TransactionAttributes
                    {
                        Transactions = new List<TransactionPartDto>
                        {
                            new TransactionPartDto
                            {
                               Description = i.ToString(),
                               Source_id = "1",
                               Destination_id = "2"
                            }
                        }
                    }
                };
            }

            _fireflyIIIService.Query = t =>
            {
                var num = int.Parse(t.Attributes.Transactions.Single().Description);
                return num < 100;
            };

            _nodeRedService.Flow = s =>
            {
                var t = JsonConvert.DeserializeObject<TransactionPartDto>(s);
                if (t.Description == "2" || t.Description == "4" )
                    t.Description = (int.Parse(t.Description) * 100).ToString();
                return (true, JsonConvert.SerializeObject(t));
            };

            var taskCompletionSource = new TaskCompletionSource();

            await _sut.StartJob(new QueryStartJobRequestDto(), async () => taskCompletionSource.TrySetResult());

            Assert.Equal(await Task.WhenAny(taskCompletionSource.Task, Task.Delay(1000)), taskCompletionSource.Task);
            Assert.Equal(RunnerState.Completed, _sut.GetStatus().State);
            Assert.Equal(2, _fireflyIIIService.UpdatedTransactions.Count);
            Assert.Equal(2, _sut.GetStatus().TotalPages);
            Assert.Equal(10, _sut.GetStatus().TotalTransactions);
            Assert.Equal(10, _sut.GetStatus().CompletedTransactions);
            Assert.Equal(10, _nodeRedService.Runs);
        }

        [Theory]
        [InlineData("1", "A", "1", "A", "D1", false, "", "", "")]
        [InlineData("1", "A", "1", "A", "D2", true, "1", "A", "D2")]
        [InlineData("1", "A", "1", "B", "D1", null, "", "", "")]
        [InlineData("1", "A", "1", "B", "D2", null, "", "", "")]
        [InlineData("1", "A", null, "A", "D1", false, "", "", "")]
        [InlineData("1", "A", null, "A", "D2", true, null, "A", "D2")]
        [InlineData("1", "A", null, "B", "D1", true, null, "B", "D1")]
        [InlineData("1", "A", null, "B", "D2", true, null, "B", "D2")]
        [InlineData("1", "B", "1", "A", "D1", null, "", "", "")]
        [InlineData("1", "B", "1", "A", "D2", null, "", "", "")]
        [InlineData("1", "B", "1", "B", "D1", false, "", "", "")]
        [InlineData("1", "B", "1", "B", "D2", true, "1", "B", "D2")]
        [InlineData("1", "B", null, "A", "D1", true, null, "A", "D1")]
        [InlineData("1", "B", null, "A", "D2", true, null, "A", "D2")]
        [InlineData("1", "B", null, "B", "D1", false, "", "", "")]
        [InlineData("1", "B", null, "B", "D2", true, null, "B", "D2")]
        [InlineData(null, "A", "1", "A", "D1", null, "", "", "")]
        [InlineData(null, "A", "1", "A", "D2", null, "", "", "")]
        [InlineData(null, "A", "1", "B", "D1", true, "1", "B", "D1")]
        [InlineData(null, "A", "1", "B", "D2", true, "1", "B", "D2")]
        [InlineData(null, "A", null, "A", "D1", false, "", "", "")]
        [InlineData(null, "A", null, "A", "D2", true, null, "A", "D2")]
        [InlineData(null, "A", null, "B", "D1", true, null, "B", "D1")]
        [InlineData(null, "A", null, "B", "D2", true, null, "B", "D2")]
        [InlineData(null, "B", "1", "A", "D1", true, "1", "A", "D1")]
        [InlineData(null, "B", "1", "A", "D2", true, "1", "A", "D2")]
        [InlineData(null, "B", "1", "B", "D1", null, "", "", "")]
        [InlineData(null, "B", "1", "B", "D2", null, "", "", "")]
        [InlineData(null, "B", null, "A", "D1", true, null, "A", "D1")]
        [InlineData(null, "B", null, "A", "D2", true, null, "A", "D2")]
        [InlineData(null, "B", null, "B", "D1", false, "", "", "")]
        [InlineData(null, "B", null, "B", "D2", true, null, "B", "D2")]
        [InlineData("1", null, "1", "A", "D1", null, "", "", "")]
        [InlineData("1", null, "1", "A", "D2", null, "", "", "")]
        [InlineData("1", null, "1", "B", "D1", null, "", "", "")]
        [InlineData("1", null, "1", "B", "D2", null, "", "", "")]
        [InlineData("1", null, null, "A", "D1", true, null, "A", "D1")]
        [InlineData("1", null, null, "A", "D2", true, null, "A", "D2")]
        [InlineData("1", null, null, "B", "D1", true, null, "B", "D1")]
        [InlineData("1", null, null, "B", "D2", true, null, "B", "D2")]
        [InlineData(null, null, "1", "A", "D1", null, "", "", "")]
        [InlineData(null, null, "1", "A", "D2", null, "", "", "")]
        [InlineData(null, null, "1", "B", "D1", null, "", "", "")]
        [InlineData(null, null, "1", "B", "D2", null, "", "", "")]
        [InlineData(null, null, null, "A", "D1", null, "", "", "")]
        [InlineData(null, null, null, "A", "D2", null, "", "", "")]
        [InlineData(null, null, null, "B", "D1", null, "", "", "")]
        [InlineData(null, null, null, "B", "D2", null, "", "", "")]
        [InlineData("1", "A", "1", null, "D1", false, "", "", "")]
        [InlineData("1", "A", "1", null, "D2", true, "1", null, "D2")]
        [InlineData("1", "A", null, null, "D1", null, "", "", "")]
        [InlineData("1", "A", null, null, "D2", null, "", "", "")]
        [InlineData("1", "B", "1", null, "D1", false, "", "", "")]
        [InlineData("1", "B", "1", null, "D2", true, "1", null, "D2")]
        [InlineData("1", "B", null, null, "D1", null, "", "", "")]
        [InlineData("1", "B", null, null, "D2", null, "", "", "")]
        [InlineData(null, "A", "1", null, "D1", true, "1", null, "D1")]
        [InlineData(null, "A", "1", null, "D2", true, "1", null, "D2")]
        [InlineData(null, "A", null, null, "D1", null, "", "", "")]
        [InlineData(null, "A", null, null, "D2", null, "", "", "")]
        [InlineData(null, "B", "1", null, "D1", true, "1", null, "D1")]
        [InlineData(null, "B", "1", null, "D2", true, "1", null, "D2")]
        [InlineData(null, "B", null, null, "D1", null, "", "", "")]
        [InlineData(null, "B", null, null, "D2", null, "", "", "")]
        [InlineData("1", null, "1", null, "D1", false, "", "", "")]
        [InlineData("1", null, "1", null, "D2", true, "1", null, "D2")]
        [InlineData("1", null, null, null, "D1", null, "", "", "")]
        [InlineData("1", null, null, null, "D2", null, "", "", "")]
        [InlineData(null, null, "1", null, "D1", null, "", "", "")]
        [InlineData(null, null, "1", null, "D2", null, "", "", "")]
        [InlineData(null, null, null, null, "D1", null, "", "", "")]
        [InlineData(null, null, null, null, "D2", null, "", "", "")]
        public async Task WillCompareTransactionPartsCorrectly(string id1, string name1, string id2, string name2, string description2, bool? expectedHasChange, string expectedId, string expectedName, string expectedDescription)
        {
            var t1 = new TransactionPartDto
            {
                Source_id = id1,
                Source_name = name1,
                Description = "D1",
                Destination_id = "DA",
                Destination_name = "DB"
            };
            var t2 = new TransactionPartDto
            {
                Source_id = id2,
                Source_name = name2,
                Description = description2,
                Destination_id = "DA",
                Destination_name = "DB"
            };

            var tid = Guid.NewGuid().ToString();
            _fireflyIIIService.Transactions[tid] = new TransactionDto
            {
                Id = tid,
                Attributes = new TransactionAttributes
                {
                    Transactions = new List<TransactionPartDto>
                    {
                        t1
                    }
                }
            };
            _fireflyIIIService.Query = t => t.Id.Equals(tid);
            _nodeRedService.Flow = _ => (true, JsonConvert.SerializeObject(t2, _serializerSettings));

            var taskCompletionSource = new TaskCompletionSource();
            await _sut.StartJob(new QueryStartJobRequestDto(), async () => taskCompletionSource.TrySetResult());
            Assert.Equal(await Task.WhenAny(taskCompletionSource.Task, Task.Delay(1000)), taskCompletionSource.Task);
            var status = _sut.GetStatus();

            if (expectedHasChange.HasValue)
            {
                Assert.Equal(RunnerState.Completed, status.State);
                if(expectedHasChange.Value)
                {
                    Assert.Collection(_fireflyIIIService.UpdatedTransactions, _ => { });
                    var result = _fireflyIIIService.Transactions[tid].Attributes.Transactions.Single();
                    Assert.Equal(expectedId, result.Source_id);
                    Assert.Equal(expectedName, result.Source_name);
                    Assert.Equal(expectedDescription, result.Description);
                }
                else
                {
                    Assert.Empty(_fireflyIIIService.UpdatedTransactions);
                }
            }
            else
            {
                Assert.Empty(_fireflyIIIService.UpdatedTransactions);
                Assert.Equal(RunnerState.Failed, status.State);
            }
        }
    }
}