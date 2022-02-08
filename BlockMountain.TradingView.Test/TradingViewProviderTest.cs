using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net.Http;

namespace BlockMountain.TradingView.Test
{
    [TestClass]
    public class TradingViewProviderTest : TestBase
    {
        [TestMethod]
        public void FindSymbols()
        {
            var response = File.ReadAllText("Resources/SymbolSearchResponse.json");
            var clientMock = SetupHttpClientTest(
                response,
                "https://demo_feed.tradingview.com/",
                "symbol_search?q=BTC&type=futures&exchange=");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.FindSymbols("BTC", "futures", "");

            Assert.IsNotNull(res);
            Assert.AreEqual(50, res.Length);
        }

        [TestMethod]
        public void GetHistory()
        {
            var response = File.ReadAllText("Resources/HistorySuccessResponse.json");
            var clientMock = SetupHttpClientTest(
                response, 
                "https://demo_feed.tradingview.com/",
                "history?symbol=BTC/USDC&resolution=60&from=1640995200&to=1643673600");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetHistory(new DateTime(2022, 01, 01), new DateTime(2022, 02, 01), "BTC/USDC", "60");

            Assert.IsNotNull(res);
            Assert.AreEqual(3, res.Close.Length);
            Assert.AreEqual(3, res.High.Length);
            Assert.AreEqual(3, res.Low.Length);
            Assert.AreEqual(3, res.Open.Length);
            Assert.AreEqual(3, res.Timestamp.Length);
        }

        [TestMethod]
        public void GetHistoryString()
        {
            var response = File.ReadAllText("Resources/HistorySuccessStringResponse.json");
            var clientMock = SetupHttpClientTest(
                response, 
                "https://demo_feed.tradingview.com/",
                "history?symbol=BTC/USDC&resolution=60&from=1640995200&to=1643673600");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetHistory(new DateTime(2022, 01, 01), new DateTime(2022, 02, 01), "BTC/USDC", "60");

            Assert.IsNotNull(res);
            Assert.AreEqual(Models.TvStatus.Ok, res.Status);
            Assert.AreEqual(3, res.Close.Length);
            Assert.AreEqual(3, res.High.Length);
            Assert.AreEqual(3, res.Low.Length);
            Assert.AreEqual(3, res.Open.Length);
            Assert.AreEqual(3, res.Timestamp.Length);
        }

        [TestMethod]
        public void GetHistoryError()
        {
            var response = File.ReadAllText("Resources/HistoryErrorResponse.json");
            var clientMock = SetupHttpClientTest(
                response, 
                "https://demo_feed.tradingview.com/",
                "history?symbol=BTC/USDC&resolution=60&from=1640995200&to=1643673600");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetHistory(new DateTime(2022, 01, 01), new DateTime(2022, 02, 01), "BTC/USDC", "60");

            Assert.IsNotNull(res);
            Assert.AreEqual(Models.TvStatus.Error, res.Status);
            Assert.AreEqual("An error occurred.", res.ErrorMessage);
        }

        [TestMethod]
        public void GetHistoryNoData()
        {
            var response = File.ReadAllText("Resources/HistoryNoDataResponse.json");
            var clientMock = SetupHttpClientTest(
                response, 
                "https://demo_feed.tradingview.com/",
                "history?symbol=BTC/USDC&resolution=60&from=1640995200&to=1643673600");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetHistory(new DateTime(2022, 01, 01), new DateTime(2022, 02, 01), "BTC/USDC", "60");

            Assert.IsNotNull(res);
            Assert.AreEqual(Models.TvStatus.NoData, res.Status);
        }

        [TestMethod]
        public void GetHistoryNextBar()
        {
            var response = File.ReadAllText("Resources/HistoryNextBarResponse.json");
            var clientMock = SetupHttpClientTest(
                response, 
                "https://demo_feed.tradingview.com/",
                "history?symbol=BTC/USDC&resolution=60&from=1640995200&to=1643673600");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetHistory(new DateTime(2022, 01, 01), new DateTime(2022, 02, 01), "BTC/USDC", "60");

            Assert.IsNotNull(res);
            Assert.AreEqual(Models.TvStatus.NoData, res.Status);
            Assert.AreEqual(1484871000, res.NextBar);
        }

        [TestMethod]
        public void GetHistoryErrorValidation()
        {
            var response = File.ReadAllText("Resources/HistoryErrorValidation.json");
            var clientMock = SetupHttpClientTest(
                response, 
                "https://demo_feed.tradingview.com/",
                "history?symbol=BTC/USDC&resolution=60&from=1640995200&to=1643673600");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetHistory(new DateTime(2022, 01, 01), new DateTime(2022, 02, 01), "BTC/USDC", "60");

            Assert.IsNotNull(res);
            Assert.AreEqual(Models.TvStatus.Error, res.Status);
            Assert.IsTrue(res.ValidSymbol.HasValue);
            Assert.IsFalse(res.ValidSymbol.Value);
            Assert.IsTrue(res.ValidResolution.HasValue);
            Assert.IsTrue(res.ValidResolution.Value);
            Assert.IsTrue(res.ValidFrom.HasValue);
            Assert.IsTrue(res.ValidFrom.Value);
        }

        [TestMethod]
        public void GetSymbol()
        {
            var response = File.ReadAllText("Resources/SymbolsResponse.json");
            var clientMock = SetupHttpClientTest(
                response,
                "https://demo_feed.tradingview.com/",
                "symbols?symbol=AAPL");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetSymbol("AAPL");

            Assert.IsNotNull(res);
            Assert.AreEqual("AAPL", res.Name);
            Assert.IsNotNull(res.SupportedResolutions);
            Assert.AreEqual("0930-1630", res.Session);
            Assert.AreEqual("America/New_York", res.Timezone);
            Assert.AreEqual("NasdaqNM", res.ExchangeListed);
            Assert.AreEqual("NasdaqNM", res.ExchangeTraded);
        }

        [TestMethod]
        public void GetTime()
        {           
            var response = File.ReadAllText("Resources/TimeResponse.json");
            var clientMock = SetupHttpClientTest(response, "https://demo_feed.tradingview.com/", "time");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetTime();

            Assert.AreNotEqual(0, res);
            Assert.AreEqual(1644248882, res);
        }

        [TestMethod]
        public void GetConfiguration()
        {
            var response = File.ReadAllText("Resources/ConfigurationResponse.json");
            var clientMock = SetupHttpClientTest(response, "https://demo_feed.tradingview.com/", "config");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetConfiguration();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.SupportMarks);
            Assert.IsTrue(res.SupportSearch);
            Assert.IsTrue(res.SupportTimeScaleMarks);
            Assert.IsTrue(res.SupportTime);
            Assert.IsFalse(res.SupportGroupRequest);
            Assert.IsNotNull(res.SymbolExchanges);
            Assert.AreEqual(5, res.SymbolExchanges.Length);
            Assert.IsNotNull(res.SymbolTypes);
            Assert.AreEqual(3, res.SymbolTypes.Length);
            Assert.IsNotNull(res.SupportedResolutions);
            Assert.AreEqual(7, res.SupportedResolutions.Length);
        }

        [TestMethod]
        public void GetConfigurationNoSymbols()
        {
            var response = File.ReadAllText("Resources/ConfigurationNoSymbolsResponse.json");
            var clientMock = SetupHttpClientTest(response, "https://serum-history.herokuapp.com/tv/", "config");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://serum-history.herokuapp.com/tv/"
            }, httpClient: httpClient);

            var res = sut.GetConfiguration();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.SupportSearch);
            Assert.IsFalse(res.SupportMarks);
            Assert.IsFalse(res.SupportTimeScaleMarks);
            Assert.IsFalse(res.SupportTime);
            Assert.IsFalse(res.SupportGroupRequest);
            Assert.IsNull(res.SymbolExchanges);
            Assert.IsNull(res.SymbolTypes);
            Assert.IsNotNull(res.SupportedResolutions);
            Assert.AreEqual(11, res.SupportedResolutions.Length);
        }

        [TestMethod]
        public void GetMarks()
        {
            var response = File.ReadAllText("Resources/MarksResponse.json");
            var clientMock = SetupHttpClientTest(response, "https://demo_feed.tradingview.com/", "marks");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetMarks();

            Assert.IsNotNull(res);
            Assert.AreEqual(6, res.Color.Length);
            Assert.AreEqual(6, res.Id.Length);
            Assert.AreEqual(6, res.Label.Length);
            Assert.AreEqual(6, res.LabelFontColor.Length);
            Assert.AreEqual(6, res.MinSize.Length);
            Assert.AreEqual(6, res.Text.Length);
            Assert.AreEqual(6, res.Timestamp.Length);
        }

        [TestMethod]
        public void GetTimescaleMarks()
        {
            var response = File.ReadAllText("Resources/TimescaleMarksResponse.json");
            var clientMock = SetupHttpClientTest(
                response, 
                "https://demo_feed.tradingview.com/",
                "timescale_marks?symbol=AAPL&resolution=60&from=1640995200&to=1643673600");
            var httpClient = new HttpClient(clientMock.Object);
            var sut = new TradingViewProvider(new()
            {
                BaseUrl = "https://demo_feed.tradingview.com/"
            }, httpClient: httpClient);

            var res = sut.GetTimescaleMarks(new DateTime(2022, 01, 01), new DateTime(2022, 02, 01), "AAPL", "60");

            Assert.IsNotNull(res);
            Assert.AreEqual(5, res.Length);
            Assert.AreEqual("tsm1", res[0].Id);
            Assert.AreEqual("red", res[0].Color);
            Assert.AreEqual("A", res[0].Label);
            Assert.AreEqual(2, res[1].Tooltip.Length);
            Assert.AreEqual("Dividends: $0.56", res[1].Tooltip[0]);
            Assert.AreEqual("Date: Fri Mar 23 2018", res[1].Tooltip[1]);
        }
    }
}
