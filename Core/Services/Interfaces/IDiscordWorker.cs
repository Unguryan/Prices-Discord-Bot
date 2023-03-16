namespace Core.Services.Interfaces
{
    /// <summary>
    /// Interface starts discord bot and api reader.
    /// </summary>
    public interface IDiscordWorker
    {
        /// <summary>
        /// Launch system.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task LaunchBotAsync(CancellationToken cancellationToken);
    }
}
