using CRMApi.Services;

namespace CRMApi;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IApiClientService, ApiClientService>();
        services.AddScoped<IApiClientService, ApiClientService>();
    }
}