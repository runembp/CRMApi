using System.Text.Json.Serialization;
using CRMApi.Interfaces;
using CRMApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CRMApi.Features.Accounts;

public static class GetAllExternalSupplierUpdated
{
    public const string Endpoint = Endpoints.GetAllExternalSupplierUpdated;

    public record GetAllExternalSupplierUpdatedRequest;

    public static void RegisterGetAllExternalSupplierUpdatedEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet(Endpoint, Handle);

    public static async Task<IResult> Handle(
        [AsParameters] GetAllExternalSupplierUpdatedRequest request,
        [FromServices] IApiClientService apiClientService)
    {
        var accountsResult = await GetUpdatedAccounts(apiClientService);
        return Results.Ok(accountsResult);
    }
    
    private static async Task<IEnumerable<Account>> GetUpdatedAccounts(
        IApiClientService apiClientService)
    {
        var odataClient = apiClientService.GetODataClient();
        var twoDaysAgo = DateTime.Now.AddDays(-2);

        var accountsResult = await odataClient
            .For<Account>()
            .Select(Entity.Properties<Account>())
            .Filter(x => x.ExternalSupplierUpdated >= twoDaysAgo)
            .FindEntriesAsync();

        return accountsResult;
    }

    [Entity(Entities.AccountEntity.EntityLogicalName)]
    public abstract class Account : IEntity
    {
        [JsonPropertyName(Entities.AccountEntity.ExternalSupplierUpdated)]
        public DateTime ExternalSupplierUpdated { get; set; }

        [JsonPropertyName(Entities.AccountEntity.FfKey)]
        public string? FfKey { get; set; }

        [JsonPropertyName(Entities.AccountEntity.RemarksAboutHealth)]
        public string? RemarksAboutHealth { get; set; }

        [JsonPropertyName(Entities.AccountEntity.ExternalSuppliers)]
        public string? ExternalSuppliers { get; set; }

        [JsonPropertyName(Entities.AccountEntity.CoveragePerEmployeeGroup)]
        public string? CoveragePerEmployeeGroup { get; set; }

        [JsonPropertyName(Entities.AccountEntity.BlumeSupport)]
        public bool? BlumeSupport { get; set; }
    }
}