using System.Net;
using System.Net.Http.Headers;
using Ardalis.GuardClauses;
using CRMApi.Interfaces;
using Simple.OData.Client;

namespace CRMApi.Services;

public class ApiClientService : IApiClientService
{
    public IODataClient GetODataClient()
    {
        var crmUsername = Environment.GetEnvironmentVariable("CRM_USERNAME");
        var crmPassword = Environment.GetEnvironmentVariable("CRM_PASSWORD");
        var domain = Environment.GetEnvironmentVariable("DOMAIN");
        var crmOrganizationService = Environment.GetEnvironmentVariable("CRM_ORGANIZATIONSERVICE_URL");

        //TODO Eventually we need a proper Environment Variable set up for the Web Api URL, instead of manupulating the organization service url...
        var removeCrmOrganizationServiceEndpoint = crmOrganizationService!.Replace("XRMServices/2011/Organization.svc", "");
        var crmWebApi = removeCrmOrganizationServiceEndpoint + "/api/data/v8.2/";

        Guard.Against.NullOrEmpty(crmUsername, nameof(crmUsername));
        Guard.Against.NullOrEmpty(crmPassword, nameof(crmPassword));
        Guard.Against.NullOrEmpty(domain, nameof(domain));
        Guard.Against.NullOrEmpty(crmOrganizationService, nameof(crmOrganizationService));

        var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(crmUsername, crmPassword, domain)
        };

        var client = new HttpClient(handler);

        client.BaseAddress = new Uri(crmWebApi);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var oDataClientSettings = new ODataClientSettings(client)
        {
            IgnoreUnmappedProperties = true
        };

        var oDataClient = new ODataClient(oDataClientSettings);

        return oDataClient;
    }
}