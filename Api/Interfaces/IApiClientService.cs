using Simple.OData.Client;

namespace CRMApi.Interfaces;

public interface IApiClientService
{
    IODataClient GetODataClient();
}