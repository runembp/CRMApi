using CRMApi.Interfaces;
using CRMApi.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRMApi.Features.Accounts;

public static class GetAllExternalSupplierUpdated
{
    private const string Endpoint = "api/accounts/GetAllExternalSupplierUpdated";

    public record GetAllExternalSupplierUpdatedRequest;

    public static void RegisterGetAllExternalSupplierUpdatedEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet(Endpoint, Handle);

    public static async Task<IResult> Handle([AsParameters] GetAllExternalSupplierUpdatedRequest request,
        [FromServices] ICrmRequestService crmRequestService, [FromServices] IApiClient apiClient)
    {
        const string query = "accounts?$filter=(Microsoft.Dynamics.CRM.LastXDays(PropertyName=%27new_externalsupplierupdated%27,PropertyValue=2))";
        var result = await crmRequestService.GetRecordsByQuery<Account>(query);

        var odataClient = apiClient.GetODataClient();
        var twoDaysAgo = DateTime.Now.AddDays(-2);

        var accountsResult = await odataClient
            .For<Account>()
            .Select()
            .Filter(x => x.ExternalSupplierUpdated >= twoDaysAgo)
            .FindEntriesAsync();

        var accounts = result.Entities;

        return Results.Ok(accounts);
    }

    [Entity("accounts")]
    public class Account : IEntity
    {
        [JsonProperty("new_externalsupplierupdated")]
        public DateTime ExternalSupplierUpdated { get; set; }

        [JsonProperty("new_ff_key")] public string? FfKey { get; set; }

        [JsonProperty("new_remarksabouthealth")]
        public string? RemarksAboutHealth { get; set; }

        [JsonProperty("new_external_suppliers")]
        public string? ExternalSuppliers { get; set; }

        [JsonProperty("new_dkningprmedarbejdergruppe")]
        public string? CoveragePerEmployeeGroup { get; set; }

        [JsonProperty("new_blumesupport")] public bool? BlumeSupport { get; set; }
    }
}