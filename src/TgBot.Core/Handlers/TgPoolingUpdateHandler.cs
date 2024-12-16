using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBot.Common.Interfaces;

namespace TgBot.Core.Handlers;
public class TgPoolingUpdateHandler(ITelegramBotClient client,
    ICallbackQueryResolver callbackQueryResolver,
    ICommandResolver commandResolver,
    ILogger<TgPoolingUpdateHandler> logger) : IUpdateHandler
{
    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        logger.LogInformation("HandleError: {Exception}", exception);
        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chatId = 0L;
        try
        {
            if (update.Type == UpdateType.Message)
            {
                chatId = update.Message.Chat.Id;
                var command = commandResolver.Resolve(update.Message.Text);
                await command.ExecuteAsync(update.Message);
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                chatId = update.CallbackQuery.Message.Chat.Id;
                var command = callbackQueryResolver.Resolve(update.CallbackQuery.Data);
                await command.ExecuteAsync(update.CallbackQuery);
            }
        }
        catch (Exception ex)
        {
            await client.SendMessage(chatId, "An error occurred. Please, try again later...");
        }
    }
}
