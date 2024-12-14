using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Common.Interfaces;

namespace TgBot.Core.Commands;
internal class StartCommand(ITelegramBotClient client) : IMessageCommand
{
    public const string Command = "/start";

    public async ValueTask ExecuteAsync(Message message)
    {
        await client.SendTextMessageAsync(message.Chat.Id, "Hello, I'm a bot!");
    }
}
