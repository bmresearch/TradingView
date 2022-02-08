using BlockMountain.TradingView;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockMountain.TradingView.Test
{
    [TestClass]
    public class TradingViewProviderFactoryTest
    {
        [TestMethod]
        public void GetProvider()
        {
            var provider = TradingViewProviderFactory.GetProvider(new TradingViewProviderConfig
            {
                BaseUrl = "https://symbol-search.tradingview.com/"
            });

            Assert.IsInstanceOfType(provider, typeof(TradingViewProvider));
            Assert.IsNotNull(provider);
            Assert.AreEqual("https://symbol-search.tradingview.com/", provider.BaseUrl);
        }
    }
}
