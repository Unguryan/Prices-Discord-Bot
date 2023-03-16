using Core.Models;

namespace Core.Services.Interfaces
{

    /// <summary>
    /// Interface for reading data for selected API that providen in <see cref="Models.BotConfig"/>.
    /// </summary>
    internal interface IApiReader
    {
        /// <summary>
        /// Gets actual data of selected token.
        /// </summary>
        /// <returns>Token model if success otherwise return null if API error.</returns>
        Task<CryptoToken?> GetLatestTokenDataAsync();
    }
}
