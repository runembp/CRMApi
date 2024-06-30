using CRMApi.Interfaces;
using CRMApi.Meta;
using CRMApi.Services;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests;

public class App : AppFixture<IApplicationMarker>
{
    public IApiClientService ApiClientService = default!;
    
    protected override Task SetupAsync()
    {
        ApiClientService = new ApiClientService();
        
        return Task.CompletedTask;
    }
    
    protected override void ConfigureApp(IWebHostBuilder a)
    {
        // do host builder config here
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
        // do test service registration here
    }

    protected override Task TearDownAsync()
    {
        return Task.CompletedTask;
    }
}