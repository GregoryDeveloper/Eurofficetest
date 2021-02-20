using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Eurofficetest.Exceptions;
using Eurofficetest.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Eurofficetest.Clients
{
    public class CatApiClient : ICatApiClient
    {
        private readonly ILogger<CatApiClient> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        
        public CatApiClient(ILogger<CatApiClient> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _configuration = configuration;

        }

        public async Task<List<Category>> GetCategoriesAsync(int limit, int page)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, GetCategoryUrl(limit, page));
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation(response?.ToString());
                throw new ApiException(response.StatusCode, response.RequestMessage?.ToString());
            }

            var body = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(body);
            return categories;
        }

        public async Task<List<Image>> GetImagesAsync(int limit, int page, int categoryId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, GetImagesUrl(limit, page, categoryId));
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation(response?.ToString());
                throw new ApiException(response.StatusCode, response.RequestMessage?.ToString());
            }

            var body = await response.Content.ReadAsStringAsync();
            var images = JsonConvert.DeserializeObject<List<Image>>(body);
            return images;
        }
        private string GetBaseUrl(string type)
        {
            var url = _configuration.GetSection("CatApiBaseUrl").Value;

            return new StringBuilder()
                .Append(url)
                .Append($"{type}")
                .ToString(); 
        }

        private string GetCategoryUrl(int limit, int page)
        {
            return new StringBuilder()
                .Append(GetBaseUrl("categories?"))
                .Append("limit=")
                .Append(limit)
                .Append("&page=")
                .Append(page)
                .ToString();
        }
        private string GetImagesUrl(int limit, int page, int categoryId)
        {
            return new StringBuilder()
                .Append(GetBaseUrl("images/"))
                .Append("search?limit=")
                .Append(limit)
                .Append("&page=")
                .Append(page)
                .Append("&category_ids=")
                .Append(categoryId)
                .ToString();
        }
    }
}
