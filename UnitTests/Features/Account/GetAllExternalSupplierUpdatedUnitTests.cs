using CRMApi.Features.Accounts;
using CRMApi.Interfaces;
using CRMApi.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;

namespace UnitTests.Features.Account;

public class GetAllExternalSupplierUpdatedUnitTests
{
    [Fact]
    public void GetAllExternalSupplierUpdated_Returns_Ok_If_Entities_Are_Found()
    {
        // Arrange
        var crmRequestService = Substitute.For<ICrmRequestService>();
        var expectedList = new ODataListEntity<GetAllExternalSupplierUpdated.Account>
        {
            Entities = new List<GetAllExternalSupplierUpdated.Account>
            {
                new()
            }
        };
        crmRequestService.GetRecordsByQuery<GetAllExternalSupplierUpdated.Account>(Arg.Any<string>()).Returns(expectedList);

        // Act
        var result = GetAllExternalSupplierUpdated.Handle(new GetAllExternalSupplierUpdated.GetAllExternalSupplierUpdatedRequest(), crmRequestService).Result;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Ok<List<GetAllExternalSupplierUpdated.Account>>>(result);

        var okResult = result as Ok<List<GetAllExternalSupplierUpdated.Account>>;
        Assert.NotNull(okResult?.Value);
        Assert.Single(okResult.Value);
    }

    [Fact]
    public void GetAllExternalSupplierUpdated_Returns_Ok_If_No_Entities_Are_Found()
    {
        // Arrange
        var crmRequestService = Substitute.For<ICrmRequestService>();
        var expectedList = new ODataListEntity<GetAllExternalSupplierUpdated.Account>();
        crmRequestService.GetRecordsByQuery<GetAllExternalSupplierUpdated.Account>(Arg.Any<string>()).Returns(expectedList);

        // Act
        var result = GetAllExternalSupplierUpdated.Handle(new GetAllExternalSupplierUpdated.GetAllExternalSupplierUpdatedRequest(), crmRequestService).Result;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Ok>(result);
        
        var okResult = result as Ok<List<GetAllExternalSupplierUpdated.Account>>;
        Assert.Null(okResult?.Value);
    }
}