using CRMApi.Context;
using Simple.OData.Client;

namespace CRMApi.Interfaces;

public interface IApiClientService
{
    IODataClient GetODataClient();
    public CrmServiceContext GetCrmServiceContext();
}