using Core.Models;
using Core.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Core.Services
{
    /// <summary>
    /// Implementation of <see cref="IApiReader"/>
    /// </summary>
    internal class ApiReader : IApiReader
    {
        /// <summary>
        /// Template for logging success reading from API.
        /// </summary>
        private const string SuccessLogTemplate = "{0}: Price: {1}, Rate:{2}";

        /// <summary>
        /// Api Url.
        /// </summary>
        private readonly string _url;

        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger<ApiReader> _logger;

        /// <summary>
        /// Initialises <see cref="ApiReader"/>
        /// </summary>
        /// <param name="botOptions">Bot Config.</param>
        /// <param name="logger">Logger.</param>
        public ApiReader(BotConfig botOptions, ILogger<ApiReader> logger)
        {
            _logger = logger;
            _url = botOptions.ApiUrl;
        }

        /// <summary>
        /// Gets actual data of selected token.
        /// </summary>
        /// <returns>Token model if success otherwise return null if API error.</returns>
        public async Task<CryptoToken?> GetLatestTokenDataAsync()
        {
            HttpClient httpClient = new HttpClient();
            using (httpClient)
            {
                try
                {
                    var resp = await httpClient.GetAsync(_url);
                    var resp_str = await resp.Content.ReadAsStringAsync();

                    var jArr = JArray.Parse(resp_str);
                    var jObj = jArr[0];
                    var priceObj = jObj.SelectToken("$.price").ToString();
                    var rateObj = jObj.SelectToken("$.price_24h_change").ToString();

                    if (decimal.TryParse(priceObj, out decimal price) &&
                        decimal.TryParse(rateObj, out decimal rate))
                    {
                        _logger.LogInformation(string.Format(SuccessLogTemplate, DateTime.Now.ToShortTimeString(), price, rate));
                        return new CryptoToken(price, rate);
                    }

                    return null;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return null;
                }
            }
        }
    }
}
