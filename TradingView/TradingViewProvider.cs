using BlockMountain.TradingView.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TradingView.Converters;
using TradingView.Models;
using TradingView.NamingPolicies;

namespace BlockMountain.TradingView
{
    /// <summary>
    /// Implements a provider for the TradingView UDF specification.
    /// </summary>
    public class TradingViewProvider : ITradingViewProvider
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The http client.
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// The json serializer options.
        /// </summary>
        private JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// The provider config.
        /// </summary>
        private TradingViewProviderConfig _providerConfig;

        /// <summary>
        /// Initialize the provider with the given config and logger.
        /// </summary>
        /// <param name="providerConfig">The provider config.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="httpClient">An http client.</param>
        internal TradingViewProvider(TradingViewProviderConfig providerConfig, ILogger logger = null, HttpClient httpClient = default)
        {
            _logger = logger;
            _providerConfig = providerConfig;
            _httpClient = httpClient ?? new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =  { new TvStatusJsonConverter() }
            };
        }

        /// <inheritdoc cref="ITradingViewProvider.BaseUrl"/>
        public string BaseUrl => _providerConfig.BaseUrl;

        /// <inheritdoc cref="ITradingViewProvider.FindSymbols(string, string, string)"/>
        public TvSymbolSearch[] FindSymbols(string query, string type, string exchange)
            => FindSymbolsAsync(query, type, exchange).Result;

        /// <inheritdoc cref="ITradingViewProvider.FindSymbolsAsync(string, string, string)"/>
        public async Task<TvSymbolSearch[]> FindSymbolsAsync(string query, string type, string exchange)
        {
            var url = _providerConfig.BaseUrl +
                   $"symbol_search?q={query}" +
                   $"&type={type}" +
                   $"&exchange={exchange}";

            HttpResponseMessage res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            return await HandleResponse<TvSymbolSearch[]>(res);
        }

        /// <inheritdoc cref="ITradingViewProvider.GetConfiguration"/>
        public TvConfiguration GetConfiguration() => GetConfigurationAsync().Result;

        /// <inheritdoc cref="ITradingViewProvider.GetConfigurationAsync"/>
        public async Task<TvConfiguration> GetConfigurationAsync()
        {
            var url = _providerConfig.BaseUrl +
                   $"config";
            HttpResponseMessage res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            return await HandleResponse<TvConfiguration>(res);
        }

        /// <inheritdoc cref="ITradingViewProvider.GetHistory(DateTime, DateTime, string, string)"/>
        public TvBarResponse GetHistory(DateTime from, DateTime to, string symbol, string resolution)
            => GetHistoryAsync(from, to, symbol, resolution).Result;

        /// <inheritdoc cref="ITradingViewProvider.GetHistoryAsync(DateTime, DateTime, string, string)"/>
        public async Task<TvBarResponse> GetHistoryAsync(DateTime from, DateTime to, string symbol, string resolution)
        {
            var url = _providerConfig.BaseUrl +
                   $"history?symbol={symbol}" +
                   $"&resolution={resolution}" +
                   $"&from={(int)(from - DateTime.UnixEpoch).TotalSeconds}" +
                   $"&to={(int)(to - DateTime.UnixEpoch).TotalSeconds}";

            HttpResponseMessage res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            return await HandleResponse<TvBarResponse>(res);
        }

        /// <inheritdoc cref="ITradingViewProvider.GetMarks"/>
        public TvMarks GetMarks()
            => GetMarksAsync().Result;

        /// <inheritdoc cref="ITradingViewProvider.GetMarksAsync"/>
        public async Task<TvMarks> GetMarksAsync()
        {
            var url = _providerConfig.BaseUrl +
                   $"marks";

            HttpResponseMessage res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            return await HandleResponse<TvMarks>(res);
        }

        /// <inheritdoc cref="ITradingViewProvider.GetTimescaleMarks(DateTime, DateTime, string, string)"/>
        public TvTimescaleMark[] GetTimescaleMarks(DateTime from, DateTime to, string symbol, string resolution)
        => GetTimescaleMarksAsync(from, to, symbol, resolution).Result;

        /// <inheritdoc cref="ITradingViewProvider.GetTimescaleMarksAsync(DateTime, DateTime, string, string)"/>
        public async Task<TvTimescaleMark[]> GetTimescaleMarksAsync(DateTime from, DateTime to, string symbol, string resolution)
        {
            var url = _providerConfig.BaseUrl +
                   $"timescale_marks?symbol={symbol}" +
                   $"&resolution={resolution}" +
                   $"&from={(int)(from - DateTime.UnixEpoch).TotalSeconds}" +
                   $"&to={(int)(to - DateTime.UnixEpoch).TotalSeconds}";

            HttpResponseMessage res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            return await HandleResponse<TvTimescaleMark[]>(res);
        }

        /// <inheritdoc cref="ITradingViewProvider.GetSymbol(string)"/>
        public TvSymbolInfo GetSymbol(string symbol) => GetSymbolAsync(symbol).Result;

        /// <inheritdoc cref="ITradingViewProvider.GetSymbolAsync(string)"/>
        public async Task<TvSymbolInfo> GetSymbolAsync(string symbol)
        {
            var url = _providerConfig.BaseUrl +
                   $"symbols?symbol={symbol}";

            HttpResponseMessage res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            return await HandleResponse<TvSymbolInfo>(res);
        }

        /// <inheritdoc cref="ITradingViewProvider.GetTime"/>
        public int GetTime() => GetTimeAsync().Result;

        /// <inheritdoc cref="ITradingViewProvider.GetTimeAsync"/>
        public async Task<int> GetTimeAsync()
        {
            var url = _providerConfig.BaseUrl +
                   $"time";

            HttpResponseMessage res = await _httpClient.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return 0;
            }

            return await HandleResponse<int>(res);
        }

        /// <summary>
        /// Handle the response to the request.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <returns>The task which returns the <see cref="RequestResult{T}"/>.</returns>
        private async Task<T> HandleResponse<T>(HttpResponseMessage message)
        {
            string data = await message.Content.ReadAsStringAsync();
            _logger?.LogInformation(new EventId(0, "REC"), $"Result: {data}");
            return JsonSerializer.Deserialize<T>(data, _jsonSerializerOptions);
        }
    }
}
