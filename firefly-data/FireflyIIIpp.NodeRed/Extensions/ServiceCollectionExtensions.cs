using FireflyIIIpp.NodeRed.Abstractions;
using FireflyIIIpp.NodeRed.Services;
using FireflyIIIpp.NodeRed.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using System.Net.Sockets;

namespace FireflyIIIpp.NodeRed.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNodeRedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NodeRedSettings>(configuration.GetSection(nameof(NodeRedSettings)));

            services.AddSingleton<INodeRedService, NodeRedService>();

            services.AddHttpClient<NodeRedService>()
                .AddPolicyHandler(GetNodeRedPolicy());

            return services;
        }
        private static IAsyncPolicy<HttpResponseMessage> GetNodeRedPolicy()
        {
            var logger = LoggerFactory.Create(builder =>
            {
                builder.ClearProviders();
                builder.AddConsole();
            }).CreateLogger<Policy>();
            var timeoutPolicy = Policy
                .TimeoutAsync<HttpResponseMessage>(1, async (ct, ts, t) =>
                {
                    logger.LogInformation("Timed out NodeRed request.");
                });
            return timeoutPolicy;
        }
    }
}
