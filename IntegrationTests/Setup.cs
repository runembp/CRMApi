using CRMApi.Interfaces;
using CRMApi.Meta;
using CRMApi.Services;
using FastEndpoints.Testing;

namespace IntegrationTests;

public class Setup : AppFixture<IApplicationMarker>
{
    public IApiClientService ApiClientService = default!;
    
    protected override Task SetupAsync()
    {
        ApiClientService = new ApiClientService();
        
        return Task.CompletedTask;
    }
}