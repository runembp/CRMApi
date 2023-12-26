using CRMApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRMApi.Features.Accounts;

public static class GetAllExternalSupplierUpdated
{
    private const string Endpoint = "api/accounts/GetAllExternalSupplierUpdated";
    private record GetAllExternalSupplierUpdatedRequest;

    public static void RegisterGetAllExternalSupplierUpdatedEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet(Endpoint, Handle);

    private static async Task<IResult> Handle([AsParameters] GetAllExternalSupplierUpdatedRequest request, 
    [FromServices] ICrmServiceEndpoint crmServiceEndpoint)
    {
        const string query = "";
        
        var result = await crmServiceEndpoint.GetRecordsByQuery(query);
        
        return Results.Ok(result);
    }
    
    public class Account
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
    }
}