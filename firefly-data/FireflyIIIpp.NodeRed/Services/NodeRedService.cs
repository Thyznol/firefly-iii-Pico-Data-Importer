using FireflyIIIpp.Core.Exceptions;
using FireflyIIIpp.Core.Extensions;
using FireflyIIIpp.NodeRed.Abstractions;
using FireflyIIIpp.NodeRed.Abstractions.Models.Dtos;
using FireflyIIIpp.NodeRed.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace FireflyIIIpp.NodeRed.Services
{
    public class NodeRedService : INodeRedService
    {
        private readonly NodeRedSettings _settings;
        private readonly ILogger<NodeRedService> _logger;
        private readonly HttpClient _httpClient;

        public NodeRedService(IOptions<NodeRedSettings> options, ILogger<NodeRedService> logger, HttpClient httpClient)
        {
            _settings = options.Value;
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }
        public async Task<HttpResponseMessage> SendToNodeRed(string path, string input, CancellationToken? cancellationToken = null)
        {
            var content = new StringContent(input, Encoding.UTF8, "application/json");
            var response = cancellationToken.HasValue
                ? await _httpClient.PostAsync(path, content, cancellationToken.Value)
                : await _httpClient.PostAsync(path, content);
            return response;
        }

        public async Task<(bool hasChanges, string output)> TryApplyRules(string input, CancellationToken? cancellationToken = null)
        {
            var response = await SendToNodeRed("/apply", input, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
                return (true, await response.Content.ReadAsStringAsync());
            else if (response.StatusCode == HttpStatusCode.NotModified)
                return (false, null);
            else
            {
                var str = await (response?.Content?.ReadAsStringAsync() ?? Task.FromResult("null"));
                throw new DownstreamException($"Node-Red returned status {response.StatusCode} with content: {str}");
            }
        }

        public async Task ExportFlows()
        {
            var response = await _httpClient.PostAsync("/export-flows", null);
            await response.EnsureDownstreamSuccessStatusCode("Node-Red");
        }

        public async Task<NodeRedExtractKeyResponseDto> ExtractKey(string field, string input, CancellationToken? cancellationToken = null)
        {
            var response = await SendToNodeRed($"/extract-key/{field}", input, cancellationToken);
            await response.EnsureDownstreamSuccessStatusCode("Node-Red");
            return await response.Content.ReadFromJsonAsync<NodeRedExtractKeyResponseDto>();
        }
    }
}
