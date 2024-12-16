using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TgBot.Core.Interfaces;

namespace LongPoolingTemplate.HostedServices;
public class PoolingHostedService(IServiceProvider serviceProvider, ILogger<PoolingHostedService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var receiver = scope.ServiceProvider.GetRequiredService<IReceiverService>();

                await receiver.ReceiveAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError("Polling failed with exception: {Exception}", ex);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}