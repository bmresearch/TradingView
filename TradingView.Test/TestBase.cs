using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BlockMountain.TradingView.Test
{
    public abstract class TestBase
    {
        /// <summary>
        /// Setup the test with the request and response data.
        /// </summary>
        /// <param name="responseContent">The response content.</param>
        protected static Mock<HttpMessageHandler> SetupHttpClientTest(string responseContent, string baseUrl, string endpoint)
        {
            var url = baseUrl + endpoint;

            var messageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            messageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(
                        message => message.Method == HttpMethod.Get && 
                        message.RequestUri.ToString() == new Uri(url).ToString()),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                })
                .Verifiable();
            return messageHandlerMock;
        }
    }
}
