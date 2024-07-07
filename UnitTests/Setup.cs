using CRMApi.Interfaces;
using CRMApi.Meta;
using CRMApi.Services;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests;

public class Setup : AppFixture<IApplicationMarker>
{
    public IApiClientService ApiClientService = default!;
    
    protected override Task SetupAsync()
    {
        ApiClientService = new ApiClientService();
        
        return Task.CompletedTask;
    }
    
    protected override void ConfigureApp(IWebHostBuilder a)
    {
        
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
        
    }

    protected override Task TearDownAsync()
    {
        return Task.CompletedTask;
    }
}