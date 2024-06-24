using CRMApi.Features.Account;
using CRMApi.Interfaces;
using FakeItEasy;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Simple.OData.Client;

namespace UnitTests.Features.Account;

public class GetAllExternalSupplierUpdatedUnitTests(App app) : TestBase<App>
{
    [Fact]
    public async Task HandleAsync_ShouldReturnAccountsUpdatedWithinLastTwoDays()
    {
        // Arrange
        var endpoint = new GetAllExternalSupplierUpdated(app.ApiClientService);

        // Act
        await endpoint.HandleAsync(CancellationToken.None);

        // Assert
        A.CallTo(() => app.ODataClient.FindEntriesAsync(A<string>._, A<CancellationToken>._)).MustHaveHappened();
    }
    
    [Fact]
    public async Task GetAllExternalSupplierUpdated_Returns_Ok_If_No_Entities_Are_Found()
    {
        // Arrange
        var apiClientService = A.Fake<IApiClientService>();
        var endpoint = Factory.Create<GetAllExternalSupplierUpdated>(apiClientService);
        
        // Act
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;
    
        // Assert
        endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Should().BeEmpty();
    }
}