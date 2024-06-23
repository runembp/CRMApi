using CRMApi.Interfaces;
using CRMApi.Meta;
using FakeItEasy;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Simple.OData.Client;

namespace UnitTests;

public class App : AppFixture<IApplicationMarker>
{
    public IApiClientService ApiClientService { get; private set; } = default!;
    public IODataClient ODataClient { get; private set; } = default!;
    
    protected override Task SetupAsync()
    {
        var apiClientService = A.Fake<IApiClientService>();
        var odataClient = A.Fake<IODataClient>();
        
        A.CallTo(() => apiClientService.GetODataClient()).Returns(odataClient);

        ApiClientService = apiClientService;
        ODataClient = odataClient;
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