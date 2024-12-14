using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.Common.Interfaces;

namespace TgBot.Core.Handlers;
public class TgUpdateHandler(ITelegramBotClient client,
    ICommandResolver commandResolver,
    ICallbackQueryResolver callbackQueryResolver) : ITgUpdateHandler
{
    public async ValueTask HandleUpdateAsync(Update update)
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
            await client.SendTextMessageAsync(chatId, "An error occurred. Please, try again later...");
        }
    }
}