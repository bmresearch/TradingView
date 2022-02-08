using BlockMountain.TradingView.Models;
using System;
using System.Threading.Tasks;
using TradingView.Models;

namespace BlockMountain.TradingView
{
    /// <summary>
    /// Specifies functionality for a TradingView data provider.
    /// </summary>
    public interface ITradingViewProvider
    {
        /// <summary>
        /// The base url of the provider.
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <returns>The provider time.</returns>
        Task<int> GetTimeAsync();

        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <returns>The provider time.</returns>
        int GetTime();

        /// <summary>
        /// Get initial configuration
        /// </summary>
        /// <returns>The provider configuration.</returns>
        Task<TvConfiguration> GetConfigurationAsync();

        /// <summary>
        /// Get initial configuration
        /// </summary>
        /// <returns>The provider configuration.</returns>
        TvConfiguration GetConfiguration();

        /// <summary>
        /// Get info for single symbol
        /// </summary>
        /// <returns>The symbol information.</returns>
        Task<TvSymbolInfo> GetSymbolAsync(string symbol);

        /// <summary>
        /// Get info for single symbol
        /// </summary>
        /// <returns>The symbol information.</returns>
        TvSymbolInfo GetSymbol(string symbol);

        /// <summary>
        /// Find available symbols by specified values
        /// </summary>
        /// <returns>The symbol information.</returns>
        Task<TvSymbolSearch[]> FindSymbolsAsync(string query, string type, string exchange);

        /// <summary>
        /// Find available symbols by specified values
        /// </summary>
        TvSymbolSearch[] FindSymbols(string query, string type, string exchange);

        /// <summary>
        /// Get historical data for specified time range.
        /// 1. Bar time for daily bars should be 00:00 UTC and is expected to be a trading day (not a day when the session starts).
        ///    Charting Library aligns the time according to the Session from SymbolInfo.  
        /// 2. Bar time for monthly bars should be 00:00 UTC and is the first trading day of the month.  
        /// 3. Prices should be passed as numbers and not as strings in quotation marks.  
        /// </summary>
        Task<TvBarResponse> GetHistoryAsync(DateTime from, DateTime to, string symbol, string resolution);

        /// <summary>
        /// Get historical data for specified time range.
        /// 1. Bar time for daily bars should be 00:00 UTC and is expected to be a trading day (not a day when the session starts).
        ///    Charting Library aligns the time according to the Session from SymbolInfo.  
        /// 2. Bar time for monthly bars should be 00:00 UTC and is the first trading day of the month.  
        /// 3. Prices should be passed as numbers and not as strings in quotation marks.  
        /// </summary>
        TvBarResponse GetHistory(DateTime from, DateTime to, string symbol, string resolution);

        /// <summary>
        /// Gets marks.  
        /// This call will be requested if your data feed sent supports_marks: true in the configuration data.
        /// </summary>
        Task<TvMarks> GetMarksAsync();

        /// <summary>
        /// Gets marks.  
        /// This call will be requested if your data feed sent supports_marks: true in the configuration data.
        /// </summary>
        TvMarks GetMarks();

        /// <summary>
        /// Get marks inside specified time range.  
        /// This call will be requested if your data feed sent supports_timescale_marks: true in the configuration data.
        /// </summary>
        Task<TvTimescaleMark[]> GetTimescaleMarksAsync(DateTime from, DateTime to, string symbol, string resolution);

        /// <summary>
        /// Get marks inside specified time range.  
        /// This call will be requested if your data feed sent supports_timescale_marks: true in the configuration data.
        /// </summary>
        TvTimescaleMark[] GetTimescaleMarks(DateTime from, DateTime to, string symbol, string resolution);
    }
}
