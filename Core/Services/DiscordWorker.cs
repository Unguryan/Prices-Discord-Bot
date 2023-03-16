using Core.Services.Interfaces;

namespace Core.Services
{
    /// <summary>
    /// Implementation of <see cref="IDiscordWorker"/>
    /// </summary>
    internal class DiscordWorker : IDiscordWorker
    {
        /// <summary>
        /// Instance of <see cref="IApiReader"/>
        /// </summary>
        private readonly IApiReader _apiReader;

        /// <summary>
        /// Instance of <see cref="IDiscordBotService"/>
        /// </summary>
        private readonly IDiscordBotService _botService;

        /// <summary>
        /// Initialises <see cref="DiscordWorker"/>
        /// </summary>
        /// <param name="apiReader">Api reader.</param>
        /// <param name="botService">Discord bot service.</param>
        public DiscordWorker(IApiReader apiReader, IDiscordBotService botService)
        {
            _apiReader = apiReader;
            _botService = botService;
        }

        /// <summary>
        /// Launch system.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task LaunchBotAsync(CancellationToken cancellationToken)
        {
            await _botService.StartBotAsync();

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var data = await _apiReader.GetLatestTokenDataAsync();

                if (data != null)
                {
                    await _botService.ChangePriceAsync(data.Price);
                    await _botService.ChangeRateAsync(data.Rate);
                }

                var timeout = 60000 - DateTime.Now.Second * 1000;
                await Task.Delay(timeout);
            }
        }
    }
}
