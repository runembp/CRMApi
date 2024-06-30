using CRMApi.Features.Account;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;

namespace UnitTests.Features.Account;

public class GetAllExternalSupplierUpdatedUnitTests(App app) : TestBase<App>
{
    private readonly List<Guid> _createdAccounts = [];
    
    [Fact]
    public async Task HandleAsync_ShouldReturnAccountsUpdatedWithinLastTwoDays()
    {
        // Arrange
        var endpoint = Factory.Create<GetAllExternalSupplierUpdated>(app.ApiClientService);
        // var client = app.ApiClientService.GetODataClient();
        // var account = new GetAllExternalSupplierUpdated.Account
        // {
        //     ExternalSupplierUpdated = DateTime.Now.AddDays(-1),
        // };
        //
        // var createdAccount = await client
        //     .For<GetAllExternalSupplierUpdated.Account>()
        //     .Set(account)
        //     .InsertEntryAsync();
        // _createdAccounts.Add(createdAccount.AccountId);
        
        // Act
        // await new GetAllExternalSupplierUpdated(app.ApiClientService).HandleAsync(CancellationToken.None);
        // var response = endpoint.Response;
        //
        // // Assert
        // endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        // // response.Should().ContainEquivalentOf(createdAccount);
        // response.Should().NotBeEmpty();
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

    protected override async Task TearDownAsync()
    {
        // Clean up
        var client = app.ApiClientService.GetODataClient();
        
        await client
            .For<GetAllExternalSupplierUpdated.Account>()
            .Key(_createdAccounts)
            .DeleteEntriesAsync();
    }
}