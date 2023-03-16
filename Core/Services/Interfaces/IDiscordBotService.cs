namespace Core.Services.Interfaces
{
    /// <summary>
    /// Interface for discord bot service.
    /// </summary>
    internal interface IDiscordBotService
    {
        /// <summary>
        /// Get info about alive bot.
        /// </summary>
        bool IsBotAlive { get; }

        /// <summary>
        /// Starts bot.
        /// </summary>
        Task StartBotAsync();

        /// <summary>
        /// Changes bot nickname to actual price.
        /// </summary>
        /// <param name="price">Actual price.</param>
        Task ChangePriceAsync(decimal price);

        /// <summary>
        /// Changes bot status to actual rate.
        /// </summary>
        /// <param name="rate">Actual rate.</param>
        Task ChangeRateAsync(decimal rate);

    }
}
