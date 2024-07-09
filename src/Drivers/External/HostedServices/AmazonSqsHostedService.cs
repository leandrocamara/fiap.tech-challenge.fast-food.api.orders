using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace External.HostedServices;

public sealed class AmazonSqsHostedService(
    IBusControl busControl,
    ILogger<AmazonSqsHostedService> logger) : IHostedService
{
    private Task? _executingTask;
    private readonly CancellationTokenSource _stoppingCts = new();

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting bus control...");

        _executingTask = Task.Run(() => busControl.StartAsync(_stoppingCts.Token), cancellationToken);
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping bus control...");
        
        if (_executingTask is not null)
        {
            await _stoppingCts.CancelAsync();
            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
            await busControl.StopAsync(cancellationToken);
        }
    }
}