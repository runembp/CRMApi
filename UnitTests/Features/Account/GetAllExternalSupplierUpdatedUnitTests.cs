using CRMApi.Features.Accounts;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;

namespace UnitTests.Features.Account;

public class GetAllExternalSupplierUpdatedUnitTests
{
    [Fact]
    public async Task GetAllExternalSupplierUpdated_Returns_Ok_And_Populated_List_If_Entities_Are_Found()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();
        var expectedList = new List<GetAllExternalSupplierUpdated.Account> { new() };
        mediator.Send(Arg.Any<GetAllExternalSupplierUpdated.GetAllExternalSupplierUpdatedQuery>(), Arg.Any<CancellationToken>()).Returns(expectedList);

        // Act
        var result = await GetAllExternalSupplierUpdated.Handle(mediator);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Ok<IEnumerable<GetAllExternalSupplierUpdated.Account>>>(result);

        var okResult = result as Ok<IEnumerable<GetAllExternalSupplierUpdated.Account>>;
        Assert.NotNull(okResult?.Value);
        Assert.Single(okResult.Value);
    }

    [Fact]
    public async Task GetAllExternalSupplierUpdated_Returns_Ok_And_Empty_List_If_No_Entities_Are_Found()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();
        mediator.Send(Arg.Any<GetAllExternalSupplierUpdated.GetAllExternalSupplierUpdatedQuery>(), Arg.Any<CancellationToken>()).Returns(new List<GetAllExternalSupplierUpdated.Account>());

        // Act
        var result = await GetAllExternalSupplierUpdated.Handle(mediator);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Ok<IEnumerable<GetAllExternalSupplierUpdated.Account>>>(result);

        var okResult = result as Ok<IEnumerable<GetAllExternalSupplierUpdated.Account>>;
        Assert.NotNull(okResult?.Value);
        Assert.Empty(okResult.Value);
    }
}