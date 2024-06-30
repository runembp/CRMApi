using CRMApi.Features.Account;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;

namespace UnitTests.Features.Account;

public class GetAllExternalSupplierUpdatedUnitTests(App app) : TestBase<App>, IAsyncLifetime
{
    private readonly List<Guid> _createdAccounts = [];
    
    [Fact]
    public async Task HandleAsync_ShouldReturnAccountsUpdatedWithinLastTwoDays()
    {
        // Arrange
        var endpoint = Factory.Create<GetAllExternalSupplierUpdated>(app.ApiClientService);
        var client = app.ApiClientService.GetODataClient();
        var account = new GetAllExternalSupplierUpdated.Account
        {
            ExternalSupplierUpdated = DateTime.Now.AddDays(-1),
            FfKey = "123",
            RemarksAboutHealth = "Remarks",
            ExternalSuppliers = "Suppliers",
            CoveragePerEmployeeGroup = "Coverage",
            BlumeSupport = true
        };
        
        var createdAccount = await client
            .For<GetAllExternalSupplierUpdated.Account>()
            .Set(account)
            .InsertEntryAsync();
        
        // Act
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;
        
        // Assert
        endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Should().ContainEquivalentOf(createdAccount);
    }
    
    [Fact]
    public async Task GetAllExternalSupplierUpdated_Returns_Ok_If_No_Entities_Are_Found()
    {
        // Arrange
        var endpoint = Factory.Create<GetAllExternalSupplierUpdated>(app.ApiClientService);
        
        // Act
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;
    
        // Assert
        endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Should().BeEmpty();
    }
    
    private async Task DisposeAsync()
    {
        // Clean up
        var client = app.ApiClientService.GetODataClient();
        
        await client
            .For<GetAllExternalSupplierUpdated.Account>()
            .Key(_createdAccounts)
            .DeleteEntriesAsync();
    }
}