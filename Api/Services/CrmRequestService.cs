using CRMApi.Interfaces;
using Simple.OData.Client;

namespace CRMApi.Services;

public class CrmRequestService(IApiClientService apiClientService)
{
    private readonly IODataClient _oDataClient = apiClientService.GetODataClient();
    
    public async Task<T> GetRecordById<T>(Guid id) where T : class, IEntity
    {
        var record = await _oDataClient
            .For<T>()
            .Key(id)
            .FindEntryAsync();

        return record;
    }
    
    public async Task<List<T>> GetRecords<T>(string query) where T : class, IEntity
    {
        var records = await _oDataClient
            .For<T>()
            .Filter(query)
            .FindEntriesAsync();

        return records.ToList();
    }
}