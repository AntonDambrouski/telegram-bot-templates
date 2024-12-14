using Telegram.Bot;
using TgBot.Common.Interfaces;
using TgBot.Core.Commands;

namespace TgBot.Core.Resolvers;
public class CommandResolver(ITelegramBotClient client) : ICommandResolver
{
    public IMessageCommand Resolve(string command) => command switch
    {
        StartCommand.Command => new StartCommand(client),
        _ => new UnknownCommand(client),
    };
}
