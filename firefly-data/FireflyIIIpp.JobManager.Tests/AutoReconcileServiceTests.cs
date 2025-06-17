using Firefly_pp_Runner.Services;
using FireflyIIIpp.Core.Models;
using FireflyIIIpp.Core.Tests.Utilities;
using FireflyIIIpp.FireflyIII.Abstractions.Models;
using FireflyIIIpp.FireflyIII.Abstractions.Models.Dtos;
using FireflyIIIpp.Tests.Fakes;
using FireflyIIIppRunner.Abstractions.AutoReconcile.Models;
using FireflyIIIppRunner.Abstractions.AutoReconcile.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FireflyIIIpp.Tests
{
    public class AutoReconcileServiceTests
    {
        private readonly FakeFireflyIIIService _fireflyIIIService;
        private readonly AutoReconcileService _sut;

        public AutoReconcileServiceTests()
        {
            _fireflyIIIService = new FakeFireflyIIIService
            {
                PageSize = 5
            };
            _sut = new AutoReconcileService(_fireflyIIIService, new FakeLogger<AutoReconcileService>());
        }

        private TransactionDto CreateTransactionDto(string id, TransactionPartDto partDto)
        {
            return new TransactionDto
            {
                Id = id,
                Attributes = new TransactionAttributes
                {
                    Transactions = new List<TransactionPartDto> { partDto }
                }
            };
        }

        private Func<TransactionDto, bool> CreateTransactionPartFilter(Func<TransactionPartDto, bool> func)
        {
            return t => func(t.Attributes.Transactions.Single());
        }

        [Fact]
        public async Task ShouldPairTransactionsWhenOnlyTwoAvailable()
        {
            _fireflyIIIService.Transactions = new Dictionary<string, TransactionDto>
            {
                { "1", CreateTransactionDto("1", new TransactionPartDto
                {
                    Description = "source tx",
                    Amount = "100",
                    Type = "withdrawal",
                    Source_name = "Acc1",
                    Destination_name = "(no name)",
                    Date = DateTime.Now.ToString("o", CultureInfo.InvariantCulture)

                }) },
                { "2", CreateTransactionDto("2", new TransactionPartDto
                {
                    Description = "dest tx",
                    Amount = "100",
                    Type = "deposit",
                    Source_name = "(no name)",
                    Destination_name = "Acc2",
                    Date = DateTime.Now.ToString("o", CultureInfo.InvariantCulture)

                }) }
            };

            _fireflyIIIService.AddQuery(CreateTransactionPartFilter(t => t.Description == "source tx"));
            _fireflyIIIService.AddQuery(CreateTransactionPartFilter(t => t.Description == "dest tx"));

            var status = await _sut.DryRun(new AutoReconcileRequestDto
            {
                PairingStrategy = new AutoReconcilePairingStrategyDto
                {
                    RequireMatchingDates = false,
                    RequireMatchingDescriptions = false
                }
            });

            var maxWaits = 5;
            var waits = 0;
            while(!(status.State == AutoReconcileState.Failed || status.State == AutoReconcileState.Completed || status.State == AutoReconcileState.Stopped))
            {
                Assert.True(waits < maxWaits);
                await Task.Delay(100);
                status = _sut.GetStatus();
                waits++;
            }

            Assert.Equal(AutoReconcileState.Completed, status.State);

            var result = await _sut.GetDryRunResult();

            Assert.Collection(result.Transfers, _ => { });
        }

        [Fact]
        public async Task ShouldPairTransactionsBasedOnAmounts()
        {
            _fireflyIIIService.Transactions = new Dictionary<string, TransactionDto>
            {
                { "1", CreateTransactionDto("1", new TransactionPartDto
                {
                    Description = "source tx",
                    Amount = "100",
                    Type = "withdrawal",
                    Source_name = "Acc1",
                    Destination_name = "(no name)",
                    Date = DateTime.Now.ToString("o", CultureInfo.InvariantCulture)

                }) },
                { "2", CreateTransactionDto("2", new TransactionPartDto
                {
                    Description = "dest tx",
                    Amount = "100",
                    Type = "deposit",
                    Source_name = "(no name)",
                    Destination_name = "Acc2",
                    Date = DateTime.Now.ToString("o", CultureInfo.InvariantCulture)

                }) },
                { "3", CreateTransactionDto("3", new TransactionPartDto
                {
                    Description = "source tx",
                    Amount = "200",
                    Type = "withdrawal",
                    Source_name = "Acc1",
                    Destination_name = "(no name)",
                    Date = DateTime.Now.ToString("o", CultureInfo.InvariantCulture)

                }) },
                { "4", CreateTransactionDto("4", new TransactionPartDto
                {
                    Description = "dest tx",
                    Amount = "200",
                    Type = "deposit",
                    Source_name = "(no name)",
                    Destination_name = "Acc2",
                    Date = DateTime.Now.ToString("o", CultureInfo.InvariantCulture)

                }) }
            };

            _fireflyIIIService.AddQuery(CreateTransactionPartFilter(t => t.Description == "source tx"));
            _fireflyIIIService.AddQuery(CreateTransactionPartFilter(t => t.Description == "dest tx"));

            var status = await _sut.DryRun(new AutoReconcileRequestDto
            {
                PairingStrategy = new AutoReconcilePairingStrategyDto
                {
                    RequireMatchingDates = false,
                    RequireMatchingDescriptions = false
                }
            });

            var maxWaits = 5;
            var waits = 0;
            while(!(status.State == AutoReconcileState.Failed || status.State == AutoReconcileState.Completed || status.State == AutoReconcileState.Stopped))
            {
                Assert.True(waits < maxWaits);
                await Task.Delay(100);
                status = _sut.GetStatus();
                waits++;
            }

            Assert.Equal(AutoReconcileState.Completed, status.State);

            var result = await _sut.GetDryRunResult();

            Assert.Collection(result.Transfers, _ => { }, _ => { });
        }

        public static IEnumerable<object[]> PairTransactionsBasedOnDateWindowData => new List<object[]>
        {
            new object[]
            {
                new List<(bool, int)>
                {
                    (true, 0),
                    (true, 7),
                    (false, 8),
                },
                new List<(int, int)>
                {
                    (1, 2)
                }
            },
            new object[]
            {
                new List<(bool, int)>
                {
                    (true, 1),
                    (false, 2),
                    (true, 10),
                    (false, 11),
                    (true, 12),
                    (false, 15),
                    (true, 15)
                },
                new List<(int, int)>
                {
                    (0, 1),
                    (6, 5)
                }
            },
            new object[]
            {
                new List<(bool, int)>
                {
                    (true, 2),
                    (true, 3),
                    (true, 7),
                    (false, 1),
                    (false, 3),
                    (false, 4),
                    (false, 6)
                },
                new List<(int, int)>
                {
                    (2, 6)
                }
            },
            new object[]
            {
                new List<(bool, int)>
                {
                },
                new List<(int, int)>
                {
                }
            },
            new object[]
            {
                new List<(bool, int)>
                {
                    (true, 0),
                },
                new List<(int, int)>
                {
                }
            },
            new object[]
            {
                new List<(bool, int)>
                {
                    (false, 8),
                },
                new List<(int, int)>
                {
                }
            },
            new object[]
            {
                new List<(bool, int)>
                {
                    (true, 0),
                    (false, 8),
                },
                new List<(int, int)>
                {
                }
            }
        };

        [Theory]
        [MemberData(nameof(PairTransactionsBasedOnDateWindowData))]
        public async Task ShouldPairTransactionsBasedOnDateWindow(List<(bool isSource, int date)> transactionGenerator, List<(int sourceId, int destId)> elementInspectorGenerator)
        {
            var now = DateTime.Now;
            TransactionDto PrepareTransactionDto(bool isSource, string id, int date) => CreateTransactionDto(id, new TransactionPartDto
            {
                Description = isSource ? "source tx" : "dest tx",
                Amount = "100",
                Type = isSource ? "withdrawal" : "deposit",
                Source_name = isSource ? $"Acc{id}" : "(no name)",
                Destination_name = isSource ? "(no name)" : $"Acc{id}",
                Date = (now + TimeSpan.FromDays(date)).ToString("o", CultureInfo.InvariantCulture)
            });

            _fireflyIIIService.Transactions = transactionGenerator
                .Select(((bool a, int b) x, int i) => (x.a, x.b, i))
                .ToDictionary(x => x.i.ToString(), x => PrepareTransactionDto(x.a, x.i.ToString(), x.b));

            _fireflyIIIService.AddQuery(CreateTransactionPartFilter(t => t.Description == "source tx"));
            _fireflyIIIService.AddQuery(CreateTransactionPartFilter(t => t.Description == "dest tx"));

            var status = await _sut.DryRun(new AutoReconcileRequestDto
            {
                PairingStrategy = new AutoReconcilePairingStrategyDto
                {
                    RequireMatchingDates = true,
                    DateMatchToleranceInDays = 1,
                    RequireMatchingDescriptions = false
                }
            });

            var maxWaits = 5;
            var waits = 0;
            while(!(status.State == AutoReconcileState.Failed || status.State == AutoReconcileState.Completed || status.State == AutoReconcileState.Stopped))
            {
                Assert.True(waits < maxWaits);
                await Task.Delay(100);
                status = _sut.GetStatus();
                waits++;
            }

            Assert.Equal(AutoReconcileState.Completed, status.State);

            var result = await _sut.GetDryRunResult();

            var elementInspectors = elementInspectorGenerator
               .Select<(int sourceId, int destId), Action<AutoReconcileTransfer>>(tup => t =>
               {
                   Assert.Equal($"Acc{tup.sourceId}", t.Source);
                   Assert.Equal($"Acc{tup.destId}", t.Destination);
               }).ToArray();

            Assert.Collection(result.Transfers, elementInspectors);

        }

    }
}
