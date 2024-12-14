namespace TgBot.Common.Interfaces;

public interface ICallbackQueryResolver
{
    ICallbackCommand Resolve(string callbackQueryData);
}