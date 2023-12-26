using CRMApi.Features.Accounts;

namespace CRMApi;

public static class EndPointsMapping
{
    public static void MapEndPoints(IEndpointRouteBuilder app)
    {
        app.RegisterGetAllExternalSupplierUpdatedEndpoint();
    }
}