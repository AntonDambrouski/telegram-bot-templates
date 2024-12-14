using Telegram.Bot.Types;

namespace TgBot.Common.Interfaces;
public interface ITgUpdateHandler
{
    ValueTask HandleUpdateAsync(Update update);
}
