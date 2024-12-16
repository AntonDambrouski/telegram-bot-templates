namespace TgBot.Core.Interfaces;
public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken token);
}
