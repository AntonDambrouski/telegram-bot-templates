using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Common.Interfaces;

namespace TgBot.Core.Commands;
internal class UnknownCommand(ITelegramBotClient client) : IMessageCommand
{
    public async ValueTask ExecuteAsync(Message message)
    {
        await client.SendTextMessageAsync(message.Chat.Id, "Unknown command");
    }
}
