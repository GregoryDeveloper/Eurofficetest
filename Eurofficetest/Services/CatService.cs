using System.Collections.Generic;
using System.Threading.Tasks;
using Eurofficetest.Clients;
using Eurofficetest.Models;

namespace Eurofficetest.Services
{
    public class CatService : ICatService
    {
        private readonly ICatApiClient _catApiClient;
        public CatService(ICatApiClient catApiClient)
        {
            _catApiClient = catApiClient;
        }
        public Task<List<Category>> GetCategoriesAsync(int limit, int page)
        {
            return _catApiClient.GetCategoriesAsync(limit, page);
        }

        public Task<List<Image>> GetImagesAsync(int limit, int page, int categoryId)
        {
            return _catApiClient.GetImagesAsync(limit, page, categoryId);
        }
    }
}
