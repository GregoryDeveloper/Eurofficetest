using Eurofficetest.Clients;
using Eurofficetest.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Eurofficetest
{
    public static class DependencyInjection
    {
        public static void Inject(this IServiceCollection services)
        {
            services.AddSingleton<ICatApiClient, CatApiClient>();
            services.AddScoped<ICatService, CatService>();
        }
    }
}
