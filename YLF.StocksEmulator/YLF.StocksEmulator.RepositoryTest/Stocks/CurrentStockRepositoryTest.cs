using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YLF.StocksEmulator.Repository.Stocks;
using System.Text.Json;
using YLF.StocksEmulator.Repository.Exceptions;
using FluentAssertions;

namespace YLF.StocksEmulator.RepositoryTest.Stocks
{
    public class CurrentStockRepositoryTests
    {
        

        [Test]
        public void GetStockAsync_EmptyString_ThrowStockNotFoundException()
        {
            //arrange
            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var currentStockMock = new CurrentStock("aaa", 100.00m, 1.0m);
            var content = JsonSerializer.Serialize(currentStockMock);
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content)
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);

            httpClientFactoryMock.Setup(f => f.CreateClient(CurrentStockRepository.HttpClientName)).Returns(client);
            CurrentStockRepository currentStockRepository = new CurrentStockRepository(httpClientFactoryMock.Object);

            string stockName = string.Empty;

            //act
            //assert

            Assert.ThrowsAsync<StockNotFoundException>( () => currentStockRepository.GetStockAsync(stockName));
        }


        [Test]
        public void GetStockAsync_NullString_ThrowStockNotFoundException()
        {
            //arrange
            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var currentStockMock = new CurrentStock("aaa", 100.00m, 1.0m);
            var content = JsonSerializer.Serialize(currentStockMock);
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content)
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);

            httpClientFactoryMock.Setup(f => f.CreateClient(CurrentStockRepository.HttpClientName)).Returns(client);
            CurrentStockRepository currentStockRepository = new CurrentStockRepository(httpClientFactoryMock.Object);

            string? stockName = null;

            //act
            //assert

            Assert.ThrowsAsync<StockNotFoundException>(() => currentStockRepository.GetStockAsync(stockName));
        }


        [Test]
        public void GetStockAsync_RandomString_ThrowStockNotFoundException()
        {
            //arrange
            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var currentStockMock = new CurrentStock("aaa", 100.00m, 1.0m);
            var content = JsonSerializer.Serialize(currentStockMock);
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = null

                }) ;
            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri($"https://{Guid.NewGuid()}")
            };

            httpClientFactoryMock.Setup(f => f.CreateClient(CurrentStockRepository.HttpClientName)).Returns(client);
            CurrentStockRepository currentStockRepository = new CurrentStockRepository(httpClientFactoryMock.Object);

            string stockName = Guid.NewGuid().ToString();

            //act
            //assert

            Assert.ThrowsAsync<StockNotFoundException>(() => currentStockRepository.GetStockAsync(stockName));
        }

        [Test]
        public async Task GetStockAsync_WithCorrectStockName_ThrowStockNotFoundException()
        {
            //arrange
            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var currentStockMock = new CurrentStock("aaa", 100.00m, 1.0m);
            var content = JsonSerializer.Serialize(currentStockMock);
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content)
                });
            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri($"https://{Guid.NewGuid()}")
            };

            httpClientFactoryMock.Setup(f => f.CreateClient(CurrentStockRepository.HttpClientName)).Returns(client);
            CurrentStockRepository currentStockRepository = new CurrentStockRepository(httpClientFactoryMock.Object);

            string stockName = currentStockMock.Name;

            //act
            var stock = await currentStockRepository.GetStockAsync(stockName);
            //assert
            stock.Should().NotBeNull();
            stock.Should().BeEquivalentTo(currentStockMock);
            
        }
    }
}
