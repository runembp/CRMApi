using CRMApi.Features.Account;

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
        Assert.Equal(expectedGetAllExternalSupplierUpdatedEndpoint, actualGetAllExternalSupplierUpdatedEndpoint);
    }
}