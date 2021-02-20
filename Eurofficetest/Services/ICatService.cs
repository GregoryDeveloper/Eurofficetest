using System.Collections.Generic;
using System.Threading.Tasks;
using Eurofficetest.Models;

namespace Eurofficetest.Services
{
    public interface ICatService
    {
        Task<List<Category>> GetCategoriesAsync(int limit, int page);
        Task<List<Image>> GetImagesAsync(int limit, int page, int categoryId);
    }
}
