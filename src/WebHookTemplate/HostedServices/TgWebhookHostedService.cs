using Microsoft.Extensions.Options;
using Telegram.Bot;
using WebhookTemplate.Configurations;

namespace WebhookTemplate.HostedServices;

public class TgWebhookHostedService(ITelegramBotClient client, IOptions<TgBotConfiguration> configs) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var webhookAddress = $"{configs.Value.WebHookUrl}/bot";
        await client.SetWebhook(
            url: webhookAddress,
            allowedUpdates: [],
            dropPendingUpdates: true,
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await client.DeleteWebhook(cancellationToken: cancellationToken);
    }
}