using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace External.HostedServices;

public sealed class AmazonSqsHostedService(
    IBusControl busControl,
    ILogger<AmazonSqsHostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting bus control...");
        await busControl.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping bus control...");
        await busControl.StopAsync(cancellationToken);
    }
}