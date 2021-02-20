using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Eurofficetest.Clients;
using Eurofficetest.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Unittests
{
    public class CatAPIClientTests
    {
        Mock<ILogger<CatApiClient>> _loggerMock;
        Mock<IHttpClientFactory> _httpClientFactoryMock;
        Mock<IConfiguration> _configurationMock;

        private const string category1 = "category1";
        private const string category2 = "category2";

        private const string url1 = "www.url1.com";
        private const string url2 = "www.url2.com";


        private const string catApiUrl= "https://api.thecatapi.com/v1/";

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CatApiClient>>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _configurationMock = new Mock<IConfiguration>();
        }

        [Test]
        public async Task GetCategoryShouldReturnCategories()
        {
            // ARRANGE
            _configurationMock.Setup(p => p.GetSection("CatApiBaseUrl").Value).Returns(catApiUrl);

            var httpClient = new Mock<HttpClient>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(GetResponseContent()),
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var weatherApiClient = new CatApiClient(_loggerMock.Object, _httpClientFactoryMock.Object, _configurationMock.Object);

            //  ACT
            var result = await weatherApiClient.GetCategoriesAsync(2,1);

            //  ASSERT
            Assert.AreEqual(category1, result[0].Name);
            Assert.AreEqual(category2, result[1].Name);
        }
        [Test]
        public async Task GetImagesShouldReturnImages()
        {
            // ARRANGE
            _configurationMock.Setup(p => p.GetSection("CatApiBaseUrl").Value).Returns(catApiUrl);

            var httpClient = new Mock<HttpClient>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(GetImagesResponseContent()),
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var weatherApiClient = new CatApiClient(_loggerMock.Object, _httpClientFactoryMock.Object, _configurationMock.Object);

            //  ACT
            var result = await weatherApiClient.GetImagesAsync(2, 1,1);

            //  ASSERT
            Assert.AreEqual(url1, result[0].Url);
            Assert.AreEqual(url2, result[1].Url);
        }

        private string GetResponseContent()
        {
            Category[] category = new Category[]
            {
                new Category{Id = 1, Name = category1},
                new Category{Id = 2, Name = category2}
            };

            return JsonConvert.SerializeObject(category);
        }

        private string GetImagesResponseContent()
        {
            Image[] images = new Image[]
            {
                new Image{Url= url1},
                new Image{Url= url2},
            };

            return JsonConvert.SerializeObject(images);
        }
    }
}