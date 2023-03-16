using Core.Models;
using Core.Services.Interfaces;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    /// <summary>
    /// Implementation of <see cref="IDiscordBotService"/>
    /// </summary>
    internal class DiscordBotService : IDiscordBotService
    {
        /// <summary>
        /// Discord nickname template. 
        /// </summary>
        private const string NicknameTemplate = "{0} at {1}$";

        /// <summary>
        /// Discord status template.
        /// </summary>
        private const string StatusTemplate = "24h rate: {0}%";

        /// <summary>
        /// Discord bot token.
        /// </summary>
        private readonly string _token;

        /// <summary>
        /// Name of token coin.
        /// </summary>
        private readonly string _coinName;

        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger<ApiReader> _logger;

        /// <summary>
        /// Discord bot client.
        /// </summary>
        private DiscordSocketClient _client;

        /// <summary>
        /// Initialises <see cref="ApiReader"/>
        /// </summary>
        /// <param name="botOptions">Bot Config.</param>
        /// <param name="logger">Logger.</param>
        public DiscordBotService(BotConfig botOptions, ILogger<ApiReader> logger)
        {
            _token = botOptions.BotToken;
            _coinName = botOptions.CoinName;
            _logger = logger;
        }

        /// <summary>
        /// Get info about is bot alive.
        /// </summary>
        public bool IsBotAlive => _client.ConnectionState == ConnectionState.Connected;

        /// <summary>
        /// Starts bot.
        /// </summary>
        public async Task StartBotAsync()
        {
            _client = new DiscordSocketClient();

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
        }

        /// <summary>
        /// Changes bot nickname to actual price.
        /// </summary>
        /// <param name="price">Actual price.</param>
        public async Task ChangePriceAsync(decimal price)
        {
            try
            {
                if (IsBotAlive)
                {
                    foreach (var res in _client.Guilds)
                    {
                        if (res != null)
                        {
                            var bot = res.Users.FirstOrDefault(u => u.Id == _client.CurrentUser.Id);
                            if (bot != null)
                            {
                                await bot.ModifyAsync(x => x.Nickname = string.Format(NicknameTemplate, _coinName, Trancate(price)));
                                Thread.Sleep(100);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        /// <summary>
        /// Changes bot status to actual rate.
        /// </summary>
        /// <param name="rate">Actual rate.</param>
        public async Task ChangeRateAsync(decimal rate)
        {
            try
            {
                if (IsBotAlive)
                {
                    await _client.SetGameAsync(string.Format(StatusTemplate, Trancate(rate)), type: ActivityType.Listening);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        /// <summary>
        /// Trancate value.
        /// </summary>
        /// <param name="value">Price or Rate.</param>
        /// <returns>Trancated value.</returns>
        private string Trancate(decimal value)
        {
            var str = value.ToString().Replace(",", ".");

            if (str.Contains("."))
            {
                var split = str.Split('.');
                return $"{split[0]}.{split[1].Substring(0, 2)}";
            }

            return $"{str}.00";
        }
    }
}
