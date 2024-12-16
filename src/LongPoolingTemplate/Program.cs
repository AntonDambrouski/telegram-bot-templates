using LongPoolingTemplate.Configurations;
using LongPoolingTemplate.HostedServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TgBot.Common.Interfaces;
using TgBot.Core.Handlers;
using TgBot.Core.Interfaces;
using TgBot.Core.Resolvers;
using TgBot.Core.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<TgBotConfiguration>(context.Configuration.GetSection(nameof(TgBotConfiguration)));

        services.AddHttpClient("telegram_bot_client").RemoveAllLoggers()
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    TgBotConfiguration? botConfiguration = sp.GetService<IOptions<TgBotConfiguration>>()?.Value;
                    ArgumentNullException.ThrowIfNull(botConfiguration);
                    TelegramBotClientOptions options = new(botConfiguration.Token);
                    return new TelegramBotClient(options, httpClient);
                });

        services.AddScoped<IUpdateHandler, TgPoolingUpdateHandler>();
        services.AddScoped<ICommandResolver, CommandResolver>();
        services.AddScoped<ICallbackQueryResolver, CallbackQueryResolver>();
        services.AddScoped<IReceiverService, ReceiverService>();
        services.AddHostedService<PoolingHostedService>();
    })
    .Build();

await host.RunAsync();