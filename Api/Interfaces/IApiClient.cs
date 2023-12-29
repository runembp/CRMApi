using Simple.OData.Client;

namespace CRMApi.Interfaces;

public interface IApiClient
{
    HttpClient GetClient();
    ODataClient GetODataClient();
}