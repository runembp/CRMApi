using CRMApi.Interfaces;
using CRMApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRMApi.Features.Accounts;

public static class GetAllExternalSupplierUpdated
{
    public const string Endpoint = Endpoints.GetAllExternalSupplierUpdated;

    public record GetAllExternalSupplierUpdatedRequest;

    public static void RegisterGetAllExternalSupplierUpdatedEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet(Endpoint, Handle);

    public static async Task<IResult> Handle(
        [AsParameters] GetAllExternalSupplierUpdatedRequest request,
        [FromServices] ICrmRequestService crmRequestService,
        [FromServices] IApiClient apiClient)
    {
        var accountsResult = await GetUpdatedAccounts(apiClient);
        return Results.Ok(accountsResult);
    }
    
    private static async Task<IEnumerable<Account>> GetUpdatedAccounts(
        IApiClient apiClient)
    {
        var odataClient = apiClient.GetODataClient();
        var twoDaysAgo = DateTime.Now.AddDays(-2);

        var accountsResult = await odataClient
            .For<Account>()
            .Select(Entity.Properties<Account>())
            .Filter(x => x.ExternalSupplierUpdated >= twoDaysAgo)
            .FindEntriesAsync();

        return accountsResult;
    }

    [Entity(Entities.AccountEntity.EntityLogicalName)]
    public class Account : IEntity
    {
        [JsonProperty(Entities.AccountEntity.ExternalSupplierUpdated)]
        public DateTime ExternalSupplierUpdated { get; set; }

        [JsonProperty(Entities.AccountEntity.FfKey)]
        public string? FfKey { get; set; }

        [JsonProperty(Entities.AccountEntity.RemarksAboutHealth)]
        public string? RemarksAboutHealth { get; set; }

        [JsonProperty(Entities.AccountEntity.ExternalSuppliers)]
        public string? ExternalSuppliers { get; set; }

        [JsonProperty(Entities.AccountEntity.CoveragePerEmployeeGroup)]
        public string? CoveragePerEmployeeGroup { get; set; }

        [JsonProperty(Entities.AccountEntity.BlumeSupport)]
        public bool? BlumeSupport { get; set; }
    }
}