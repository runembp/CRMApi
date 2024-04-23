using System.Text.Json.Serialization;
using CRMApi.Interfaces;
using CRMApi.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRMApi.Features.Accounts;

public static class GetAllExternalSupplierUpdated
{
    public const string Endpoint = Endpoints.GetAllExternalSupplierUpdated;

    public static void RegisterGetAllExternalSupplierUpdatedEndpoint(this IEndpointRouteBuilder endpoints) => endpoints.MapGet(Endpoint, Handle);

    public record GetAllExternalSupplierUpdatedQuery : IRequest<IEnumerable<Account>>;

    public static async Task<IResult> Handle([FromServices] IMediator mediator)
    {
        var accountsResult = await mediator.Send(new GetAllExternalSupplierUpdatedQuery());

        return Results.Ok(accountsResult);
    }

    public class GetAllExternalSupplierUpdatedHandler(IApiClientService apiClient) : IRequestHandler<GetAllExternalSupplierUpdatedQuery, IEnumerable<Account>>
    {
        public async Task<IEnumerable<Account>> Handle(GetAllExternalSupplierUpdatedQuery request, CancellationToken cancellationToken)
        {
            var odataClient = apiClient.GetODataClient();
            var twoDaysAgo = DateTime.Now.AddDays(-2);

            var accountsResult = await odataClient
                .For<Account>()
                .Select(Entity.JsonProperties<Account>())
                .Filter(x => x.ExternalSupplierUpdated >= twoDaysAgo)
                .FindEntriesAsync(cancellationToken);

            accountsResult = accountsResult.ToList();

            TrimAndRemoveNewLines(accountsResult);

            return accountsResult;
        }

        private static void TrimAndRemoveNewLines(IEnumerable<Account> accountsResult)
        {
            foreach (var account in accountsResult)
            {
                var properties = typeof(Account).GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType != typeof(string)) continue;

                    if (property.GetValue(account) is not string value) continue;

                    value = value.Trim().Replace("\n", "");
                    property.SetValue(account, value);
                }
            }
        }
    }

    [Entity(Entities.AccountEntity.EntityLogicalName)]
    public class Account
    {
        [JsonPropertyName(Entities.AccountEntity.ExternalSupplierUpdated)]
        public DateTime? ExternalSupplierUpdated { get; set; }

        [JsonPropertyName(Entities.AccountEntity.FfKey)]
        public string? FfKey { get; set; }

        [JsonPropertyName(Entities.AccountEntity.RemarksAboutHealth)]
        public string? RemarksAboutHealth { get; set; }

        [JsonPropertyName(Entities.AccountEntity.ExternalSuppliers)]
        public string? ExternalSuppliers { get; set; }

        [JsonPropertyName(Entities.AccountEntity.CoveragePerEmployeeGroup)]
        public string? CoveragePerEmployeeGroup { get; set; }

        [JsonPropertyName(Entities.AccountEntity.BlumeSupport)]
        public string? BlumeSupport { get; set; }
    }
}