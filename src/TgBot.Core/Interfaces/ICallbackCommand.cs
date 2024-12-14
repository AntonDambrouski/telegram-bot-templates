using Telegram.Bot.Types;

namespace TgBot.Common.Interfaces;
public interface ICallbackCommand
{
    ValueTask ExecuteAsync(CallbackQuery query);
}