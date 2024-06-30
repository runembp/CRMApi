using System.Net;
using System.Xml;
using Microsoft.OData.Client;

namespace CRMApi.Context;

public sealed class CrmServiceContext : DataServiceContext
{
    public CrmServiceContext(Uri serviceRoot, NetworkCredential networkCredential) : base(serviceRoot)
    {
        Credentials = networkCredential;
        Accounts = CreateQuery<AccountEntity>(AccountEntity.EntityPluralName);
    }
    
    public CrmServiceContext(Uri serviceRoot, string username, string password) : base(serviceRoot)
    {
        HttpClient = new HttpClient(new CustomHttpClientHandler())
        {
            BaseAddress = serviceRoot,
            DefaultRequestHeaders = { Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"))) }
        };
    }
    
    public DataServiceQuery<AccountEntity> Accounts { get; }
}

public class CustomHttpClientHandler : HttpClientHandler
{
    protected override HttpResponseMessage Send(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        var response = base.Send(request, cancellationToken);
        
        // Configure XML Reader Settings
        var settings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Parse
        };

        // Ensure the response content uses these settings
        response.Content = new StreamContent(XmlReader.Create(response.Content.ReadAsStream(), settings));

        return response;
    }
}