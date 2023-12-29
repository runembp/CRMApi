using CRMApi;
using CRMApi.Features.Accounts;
using Microsoft.AspNetCore.Routing;
using NSubstitute;

namespace UnitTests.Endpoints;

public class EndPointsVerifier
{
    [Fact]
    public void EndPoints_Are_Set_To_Correct_Values()
    {
        // Arrange
        const string expectedGetAllExternalSupplierUpdatedEndpoint = "api/accounts/GetAllExternalSupplierUpdated";

        // Act
        const string actualGetAllExternalSupplierUpdatedEndpoint = GetAllExternalSupplierUpdated.Endpoint;

        // Assert
        Assert.Equal(expectedGetAllExternalSupplierUpdatedEndpoint, actualGetAllExternalSupplierUpdatedEndpoint);
    }
    
    [Fact]
    public void EndPoints_Are_Registered_Correctly()
    {
        // Arrange
        var endpoints = Substitute.For<IEndpointRouteBuilder>();

        // Act
        EndPointsMapping.MapEndPoints(endpoints);

        // Assert
        endpoints.Received(1).RegisterGetAllExternalSupplierUpdatedEndpoint();
    }
}