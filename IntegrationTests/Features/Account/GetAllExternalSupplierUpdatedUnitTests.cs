using CRMApi.Features.Account;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;

namespace IntegrationTests.Features.Account;

public class GetAllExternalSupplierUpdatedUnitTests(Setup setup) : TestBase<Setup>, IAsyncLifetime
{
    private readonly List<Guid> _createdAccounts = [];
    
    [Fact]
    public async Task HandleAsync_ShouldReturnAccountsUpdatedWithinLastTwoDays()
    {
        // Arrange
        var odataClient = setup.ApiClientService.GetODataClient();
        var endpoint = Factory.Create<GetAllExternalSupplierUpdated>(odataClient);
        
        // Arrange
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;

        // Assert
        endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetAllExternalSupplierUpdated_Returns_Ok_If_No_Entities_Are_Found()
    {
        // Arrange
        await CreateAccount(DateTime.Now.AddDays(-3));
        var endpoint = Factory.Create<GetAllExternalSupplierUpdated>(setup.ApiClientService);
        
        // Act
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;
    
        // Assert
        endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Should().BeEmpty();
    }

    private async Task CreateAccount(DateTime date)
    {
        var client = setup.ApiClientService.GetODataClient();

        var account = new GetAllExternalSupplierUpdated.Account
        {
            ExternalSupplierUpdated = date
        };

        var thing = await client
            .For<GetAllExternalSupplierUpdated.Account>()
            .Set(account)
            .InsertEntryAsync(true);
    }

    public async Task DisposeAsync()
    {
        // Clean up
        var client = setup.ApiClientService.GetODataClient();
        
        await client
            .For<GetAllExternalSupplierUpdated.Account>()
            .Key(_createdAccounts)
            .DeleteEntriesAsync();
    }
}