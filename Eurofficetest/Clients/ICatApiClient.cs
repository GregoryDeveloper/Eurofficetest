using System.Collections.Generic;
using System.Threading.Tasks;
using Eurofficetest.Models;

namespace Eurofficetest.Clients
{
    public interface ICatApiClient
    {
        public Task<List<Category>> GetCategoriesAsync(int limit, int page);
        public Task<List<Image>> GetImagesAsync(int limit, int page, int categoryId);
    }
}
