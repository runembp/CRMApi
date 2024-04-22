using CRMApi.Features.Accounts;
using CRMApi.Interfaces;
using NSubstitute;

namespace UnitTests.Features.Account;

public class GetAllExternalSupplierUpdatedUnitTests
{
    [Fact]
    public async Task GetAllExternalSupplierUpdated_Returns_Ok_If_Entities_Are_Found()
    {
        // Arrange
        var apiClientService = Substitute.For<IApiClientService>();
        var request = new GetAllExternalSupplierUpdated.GetAllExternalSupplierUpdatedRequest();
        
        // Act
        var result = await GetAllExternalSupplierUpdated.Handle(new GetAllExternalSupplierUpdated.GetAllExternalSupplierUpdatedRequest(), apiClientService);

        // Assert

    }

    // [Fact]
    // public void GetAllExternalSupplierUpdated_Returns_Ok_If_No_Entities_Are_Found()
    // {
    //     // Arrange
    //     var crmRequestService = Substitute.For<ICrmRequestService>();
    //     var apiClientService = Substitute.For<IApiClientService>();
    //     var expectedList = new List<GetAllExternalSupplierUpdated.Account>();
    //     crmRequestService.GetRecords<GetAllExternalSupplierUpdated.Account>(Arg.Any<string>()).Returns(expectedList);
    //
    //     // Act
    //     var result = GetAllExternalSupplierUpdated.Handle(new GetAllExternalSupplierUpdated.GetAllExternalSupplierUpdatedRequest(), crmRequestService, apiClientService).Result;
    //
    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.IsType<Ok>(result);
    //
    //     var okResult = result as Ok<List<GetAllExternalSupplierUpdated.Account>>;
    //     Assert.Null(okResult?.Value);
    // }
}