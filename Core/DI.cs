using Core.Models;
using Core.Services;
using Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    /// <summary>
    /// Dependency injection.
    /// </summary>
    public static class DI
    {
        /// <summary>
        /// Adds all required services.
        /// </summary>
        /// <param name="services">Service builder.</param>
        /// <param name="configuration">Configuration.</param>
        public static void AddDiscord(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: Add BotConfig normally, using Configure().Bind()
            var botToken = configuration.GetSection(BotConfig.SectionName+ ":BotToken").Value!;
            var coinName = configuration.GetSection(BotConfig.SectionName+ ":CoinName").Value!;
            var apiUrl = configuration.GetSection(BotConfig.SectionName+ ":ApiUrl").Value!;
            services.AddSingleton(new BotConfig() { BotToken = botToken, CoinName = coinName, ApiUrl = apiUrl });

            services.AddScoped<IApiReader, ApiReader>();
            services.AddScoped<IDiscordBotService, DiscordBotService>();
            services.AddScoped<IDiscordWorker, DiscordWorker>();
        }
    }
}
