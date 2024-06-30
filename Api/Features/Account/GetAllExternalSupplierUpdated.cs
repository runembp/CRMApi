using CRMApi.Context;
using Microsoft.OData.Client;

namespace CRMApi.Features.Account;

public class GetAllExternalSupplierUpdated(IApiClientService apiClientService) : EndpointWithoutRequest<IEnumerable<GetAllExternalSupplierUpdated.Account>>
{
    public override void Configure()
    {
        Get(AccountConstants.GetAllExternalSupplierUpdated);
        Tags(AccountConstants.Tag);
        AllowAnonymous();

        Description(endpoint => endpoint.Produces<IEnumerable<Account>>());
        Summary(summary => summary.Summary = "Retrieves Accounts where the field ExternalSupplierUpdated has been updated within the last two days");
        Summary(summary => summary.Responses[200] = "Either a populated list or empty list");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var context = apiClientService.GetCrmServiceContext();
        
        // var odataClient = apiClientService.GetODataClient();
        // var twoDaysAgo = DateTime.Now.Date.AddDays(-2).ToString("yyyy-mm-ddThh:mm:ss('.'s+)?(zzzzzz)?");
        //
        // var accountsResult = odataClient
        //     .For<Account>()
        //     .Select(EntityUtility.Properties<Account>())
        //     .Filter(x => x.ExternalSupplierUpdated >= DateTime.Now.Date);
        //
        // var accountsCommand = await accountsResult.GetCommandTextAsync();
        //
        // var thing = await accountsResult.FindEntriesAsync(cancellationToken);
        
        var test = context.Accounts
            .Where(x => x.ExternalSupplierUpdated >= DateTime.Now.Date)
            .ToList();

        var query = new List<Account>(); 

        await SendOkAsync(query, cancellationToken);
    }

    [Entity(AccountEntity.EntityLogicalName)]
    public class Account
    {
        [JsonPropertyName(AccountEntity.PrimaryKey)]
        public Guid AccountId { get; init; }

        [JsonPropertyName(AccountEntity.FieldExternalSupplierUpdated)]
        public DateTime ExternalSupplierUpdated { get; init; }

        [JsonPropertyName(AccountEntity.FieldFfKey)]
        public string? FfKey { get; init; }

        [JsonPropertyName(AccountEntity.FieldRemarksAboutHealth)]
        public string? RemarksAboutHealth { get; init; }

        [JsonPropertyName(AccountEntity.FieldExternalSuppliers)]
        public string? ExternalSuppliers { get; init; }

        [JsonPropertyName(AccountEntity.FieldCoveragePerEmployeeGroup)]
        public string? CoveragePerEmployeeGroup { get; init; }

        [JsonPropertyName(AccountEntity.FieldBlumeSupport)]
        public bool? BlumeSupport { get; init; }
    }
}