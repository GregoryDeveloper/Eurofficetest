using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace Unittests
{
    public class Tests
    {
        Mock<ILogger<WeatherApiClient>> _loggerMock;
        Mock<IHttpClientFactory> _httpClientFactoryMock;
        Mock<IConfiguration> _configurationMock;
        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<WeatherApiClient>>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _configurationMock = new Mock<IConfiguration>();
        }

        [Test]
        public async Task GetCurrentWeatherConditionInShouldReturnWeather()
        {
            // ARRANGE
            _configurationMock.Setup(p => p.GetSection("WeatherApiBaseUrl").Value).Returns(weatherApiUrl);
            
            var httpClient = new Mock<HttpClient>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(GetresponseContent()),
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var weatherApiClient = new WeatherApiClient(_loggerMock.Object, _httpClientFactoryMock.Object, _configurationMock.Object);

            //  ACT
            var result = await weatherApiClient.GetCurrentWeatherConditionIn("London");

            //  ASSERT
            Assert.AreEqual(name, result.Location.Name);
            Assert.AreEqual(region, result.Location.Region);
            Assert.AreEqual(country, result.Location.Country);
        }

        private string GetResponseContent()
        {
            Category category = new Weather
            {
                Location = new Location
                {
                    Name = name,
                    Region = region,
                    Country = country,
                    Localtime = new DateTime(2020, 11, 29)
                },
                Current = new Current
                {
                    TempInCelsius = 8.0M
                }
            };

            return JsonConvert.SerializeObject(category);
        }
    }