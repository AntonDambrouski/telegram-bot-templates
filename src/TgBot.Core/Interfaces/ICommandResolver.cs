namespace TgBot.Common.Interfaces;
public interface ICommandResolver
{
    IMessageCommand Resolve(string command);
}