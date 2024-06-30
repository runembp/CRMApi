using System.Net;
using System.Net.Http.Headers;
using CRMApi.Context;
using Simple.OData.Client;

namespace CRMApi.Services;

public class ApiClientService : IApiClientService
{
    public IODataClient GetODataClient()
    {
        var crmUsername = Environment.GetEnvironmentVariable("CRM_USERNAME");
        var crmPassword = Environment.GetEnvironmentVariable("CRM_PASSWORD");
        var domain = Environment.GetEnvironmentVariable("DOMAIN");
        var crmOrganizationServiceEndpoint = Environment.GetEnvironmentVariable("CRM_ORGANIZATIONSERVICE_URL");

        ArgumentNullException.ThrowIfNull(crmUsername, nameof(crmUsername));
        ArgumentNullException.ThrowIfNull(crmPassword, nameof(crmPassword));
        ArgumentNullException.ThrowIfNull(domain, nameof(domain));
        ArgumentNullException.ThrowIfNull(crmOrganizationServiceEndpoint, nameof(crmOrganizationServiceEndpoint));

        //TODO Eventually we need a proper Environment Variable set up for the Web Api URL, instead of manipulating the organization service url...
        var crmUrl = crmOrganizationServiceEndpoint.Replace("XRMServices/2011/Organization.svc", string.Empty);
        var crmWebApiEndpoint = $"{crmUrl}/api/data/v8.2/";

        var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(crmUsername, crmPassword, domain)
        };

        var client = new HttpClient(handler);

        client.BaseAddress = new Uri(crmWebApiEndpoint);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var oDataClientSettings = new ODataClientSettings(client)
        {
            IgnoreUnmappedProperties = true
        };

        return new ODataClient(oDataClientSettings);
    }
    
    public CrmServiceContext GetCrmServiceContext()
    {
        var crmUsername = Environment.GetEnvironmentVariable("CRM_USERNAME");
        var crmPassword = Environment.GetEnvironmentVariable("CRM_PASSWORD");
        var domain = Environment.GetEnvironmentVariable("DOMAIN");
        var crmOrganizationServiceEndpoint = Environment.GetEnvironmentVariable("CRM_ORGANIZATIONSERVICE_URL");

        ArgumentNullException.ThrowIfNull(crmUsername, nameof(crmUsername));
        ArgumentNullException.ThrowIfNull(crmPassword, nameof(crmPassword));
        ArgumentNullException.ThrowIfNull(domain, nameof(domain));
        ArgumentNullException.ThrowIfNull(crmOrganizationServiceEndpoint, nameof(crmOrganizationServiceEndpoint));

        var crmUrl = crmOrganizationServiceEndpoint.Replace("XRMServices/2011/Organization.svc", string.Empty);
        var crmWebApiEndpoint = $"{crmUrl}/api/data/v8.2/";
        
        var networkCredentials = new NetworkCredential(crmUsername, crmPassword, domain);

        return new CrmServiceContext(new Uri(crmWebApiEndpoint), networkCredentials);
    }
}