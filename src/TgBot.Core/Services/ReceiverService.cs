using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TgBot.Core.Interfaces;

namespace TgBot.Core.Services;
public class ReceiverService(ITelegramBotClient client,
    IUpdateHandler updateHandler,
    ILogger<ReceiverService> logger) : IReceiverService
{
    public async Task ReceiveAsync(CancellationToken token)
    {
        var receiverOptions = new ReceiverOptions() { DropPendingUpdates = true, AllowedUpdates = [] };

        logger.LogInformation("Start receiving updates");

        await client.ReceiveAsync(updateHandler, receiverOptions, token);
    }
}
