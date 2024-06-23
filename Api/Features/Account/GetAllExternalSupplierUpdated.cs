namespace CRMApi.Features.Account;

public class GetAllExternalSupplierUpdated(IApiClientService apiClientService) : EndpointWithoutRequest<IEnumerable<GetAllExternalSupplierUpdated.Account>>
{
    public override void Configure()
    {
        Get(AccountConstants.GetAllExternalSupplierUpdated);
        Tags(AccountConstants.Tag);

        Description(endpoint => endpoint.Produces<IEnumerable<Account>>());
        Summary(summary => summary.Summary = "Retrieves Accounts where the field ExternalSupplierUpdated has been updated within the last two days");
        Summary(summary => summary.Responses[200] = "Either a populated list or empty list");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var odataClient = apiClientService.GetODataClient();
        var twoDaysAgo = DateTime.Now.AddDays(-2);

        var accountsResult = await odataClient
            .For<Account>()
            .Select(Entity.Properties<Account>())
            .Filter(x => x.ExternalSupplierUpdated >= twoDaysAgo)
            .FindEntriesAsync(cancellationToken);

        await SendOkAsync(accountsResult, cancellationToken);
    }

    [Entity(AccountEntity.EntityLogicalName)]
    public class Account
    {
        [JsonPropertyName(AccountEntity.ExternalSupplierUpdated)]
        public DateTime ExternalSupplierUpdated { get; init; }

        [JsonPropertyName(AccountEntity.FfKey)]
        public string? FfKey { get; init; }

        [JsonPropertyName(AccountEntity.RemarksAboutHealth)]
        public string? RemarksAboutHealth { get; init; }

        [JsonPropertyName(AccountEntity.ExternalSuppliers)]
        public string? ExternalSuppliers { get; init; }

        [JsonPropertyName(AccountEntity.CoveragePerEmployeeGroup)]
        public string? CoveragePerEmployeeGroup { get; init; }

        [JsonPropertyName(AccountEntity.BlumeSupport)]
        public bool? BlumeSupport { get; init; }
    }
}