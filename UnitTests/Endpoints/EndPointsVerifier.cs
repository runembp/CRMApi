using CRMApi.Features.Account;
using FluentAssertions;

namespace UnitTests.Endpoints;

public class EndPointsVerifier
{
    [Fact]
    public void EndPoints_Are_Set_To_Correct_Values()
    {
        // Arrange
        const string expectedGetAllExternalSupplierUpdatedEndpoint = "accounts/GetAllExternalSupplierUpdated";

        // Act
        const string actualGetAllExternalSupplierUpdatedEndpoint = AccountConstants.GetAllExternalSupplierUpdated;

        // Assert
        expectedGetAllExternalSupplierUpdatedEndpoint.Should().BeEquivalentTo(actualGetAllExternalSupplierUpdatedEndpoint);
    }
}