using CRMApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        const string query = "accounts?$filter=(Microsoft.Dynamics.CRM.LastXDays(PropertyName=%27new_externalsupplierupdated%27,PropertyValue=2))";
        var result = await crmServiceEndpoint.GetRecordsByQuery<Account>(query);

        var accounts = result.Entities;
        
        return Results.Ok(accounts);
    }
    
    private class Account : IEntity
    {
        [JsonProperty("new_ff_key")]
        public string? FfKey { get; set; }
        
        [JsonProperty("new_remarksabouthealth")]
        public string? RemarksAboutHealth { get; set; }
            
        [JsonProperty("new_external_suppliers")]
        public string? ExternalSuppliers { get; set; }
        
        [JsonProperty("new_dkningprmedarbejdergruppe")]
        public string? CoveragePerEmployeeGroup { get; set; }
        
        [JsonProperty("new_blumesupport")]
        public bool? BlumeSupport { get; set; }
    }
}