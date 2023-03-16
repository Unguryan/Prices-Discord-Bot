namespace Core.Models
{
    /// <summary>
    /// Provides Discord bot config.
    /// </summary>
    internal class BotConfig
    {
        /// <summary>
        /// Name in appsettings.json
        /// </summary>
        internal static readonly string SectionName = "BotConfiguration";

        /// <summary>
        /// Provides token of discord bot.
        /// </summary>
        internal string BotToken { get; set; } = default!;
        
        /// <summary>
        /// Provides name of coin.
        /// </summary>
        internal string CoinName { get; set; } = default!;

        /// <summary>
        /// Url for reading token data. Use: https://api-osmosis.imperator.co/tokens/v2/ 
        /// </summary>
        internal string ApiUrl { get; set; } = default!;
    }
}
