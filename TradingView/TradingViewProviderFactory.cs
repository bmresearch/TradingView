using Microsoft.Extensions.Logging;

namespace BlockMountain.TradingView
{
    /// <summary>
    /// A factory for <see cref="TradingViewProvider"/>.
    /// </summary>
    public static class TradingViewProviderFactory
    {
        /// <summary>
        /// Get a provider with the given config.
        /// </summary>
        /// <param name="config">The provider config.</param>
        /// <param name="logger">The logger instance.</param>
        /// <returns>The <see cref="TradingViewProvider"/>.</returns>
        public static TradingViewProvider GetProvider(TradingViewProviderConfig config, ILogger logger = null)
        {
            return new TradingViewProvider(config, logger);
        }
    }
}
