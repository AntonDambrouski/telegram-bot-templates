using TgBot.Common.Interfaces;

namespace TgBot.Core.Resolvers;
public class CallbackQueryResolver : ICallbackQueryResolver
{
    public ICallbackCommand Resolve(string callbackQueryData)
    {
        throw new NotImplementedException();
    }
}
