using Core;
using Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.ConfigureServices(services =>
{
    services.AddDiscord(config);
});

var host = builder.Build();

var worker = host.Services.GetRequiredService<IDiscordWorker>();

var token = CancellationToken.None;
await worker.LaunchBotAsync(token);

await host.RunAsync();


