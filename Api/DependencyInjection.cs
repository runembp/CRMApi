using CRMApi.Interfaces;
using CRMApi.Services;

namespace CRMApi;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddSingleton<ICrmServiceEndpoint, CrmServiceEndpoint>();
        services.AddSingleton<IApiClient, ApiClient>();
    }
}