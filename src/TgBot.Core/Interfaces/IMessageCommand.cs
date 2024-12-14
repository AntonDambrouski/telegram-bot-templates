using Telegram.Bot.Types;

namespace TgBot.Common.Interfaces;
public interface IMessageCommand
{
    ValueTask ExecuteAsync(Message message);
}
