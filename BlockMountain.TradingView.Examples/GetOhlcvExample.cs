using System;

namespace BlockMountain.TradingView.Examples
{
    public class GetOhlcvExample : IRunnableExample
    {
        private ITradingViewProvider _provider = TradingViewProviderFactory.GetProvider(new TradingViewProviderConfig
        {
            BaseUrl = "https://serum-history.herokuapp.com/tv/"
        });
        private ITradingViewProvider _tvProvider = TradingViewProviderFactory.GetProvider(new TradingViewProviderConfig
        {
            BaseUrl = "https://symbol-search.tradingview.com/"
        });

        public void Run()
        {
            var shConfig = _provider.GetConfiguration();

            // btc bars
            var bars = _provider.GetHistory(new DateTime(2022, 02, 01), DateTime.UtcNow, "BTC/USDC", "60");

            // search TV API
            var search = _tvProvider.FindSymbols("S&P", "futures", "");

        }
    }
}
