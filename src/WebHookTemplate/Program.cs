using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Common.Interfaces;
using TgBot.Core.Handlers;
using TgBot.Core.Resolvers;
using WebhookTemplate.Configurations;
using WebhookTemplate.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TgBotConfiguration>(builder.Configuration.GetSection(nameof(TgBotConfiguration)));
builder.Services.AddHostedService<TgWebhookHostedService>();
builder.Services.AddScoped<ITgUpdateHandler, TgUpdateHandler>();
builder.Services.AddScoped<ICommandResolver, CommandResolver>();
builder.Services.AddScoped<ICallbackQueryResolver, CallbackQueryResolver>();

var token = builder.Configuration["TgBotConfiguration:Token"];
builder.Services.ConfigureTelegramBot<Microsoft.AspNetCore.Http.Json.JsonOptions>(opt => opt.SerializerOptions);
builder.Services.AddHttpClient("tgwebhook")
    .RemoveAllLoggers()
    .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(token, httpClient));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapPost("/bot", async([FromBody] Update update, [FromServices] ITgUpdateHandler handler)
    => await handler.HandleUpdateAsync(update));

app.Run();