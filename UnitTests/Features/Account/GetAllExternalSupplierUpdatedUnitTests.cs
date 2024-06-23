using CRMApi.Features.Account;
using CRMApi.Interfaces;
using FakeItEasy;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;

namespace UnitTests.Features.Account;

public class GetAllExternalSupplierUpdatedUnitTests(App app) : TestBase<App>
{
    [Fact]
    public async Task GetAllExternalSupplierUpdated_Returns_Ok_If_Entities_Are_Found()
    {
        var endpoint = Factory.Create<GetAllExternalSupplierUpdated>(app.ApiClientService);
        
        // Act
        await endpoint.HandleAsync(default);
        var response = endpoint.Response.ToList();
        
        // Assert
        endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        endpoint.HttpContext.Response.ContentType.Should().Be("application/json");
        response.Should().NotBeNull();
        response.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task GetAllExternalSupplierUpdated_Returns_Ok_If_No_Entities_Are_Found()
    {
        // Arrange
        var apiClientService = A.Fake<IApiClientService>();
        var endpoint = Factory.Create<GetAllExternalSupplierUpdated>(apiClientService);
        
        // Act
        await endpoint.HandleAsync(default);
        var response = endpoint.Response;
    
        // Assert
        endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Should().BeEmpty();
    }
}