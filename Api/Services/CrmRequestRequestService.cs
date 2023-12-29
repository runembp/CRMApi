using Ardalis.GuardClauses;
using CRMApi.Entities;
using CRMApi.Interfaces;
using Newtonsoft.Json;
using Simple.OData.Client;

namespace CRMApi.Services;

public class CrmRequestRequestService(IApiClient apiClient) : ICrmRequestService
{
    private readonly HttpClient _httpClient = apiClient.GetClient();
    private readonly ODataClient _oDataClient = apiClient.GetODataClient();

    private readonly JsonSerializerSettings _jsonSettings = new()
    {
        MissingMemberHandling = MissingMemberHandling.Ignore
    };

    public async Task<ODataRecordEntity<T>> GetRecordByQuery<T>(string query) where T : IEntity
    {
        var jsonResponse = await GetJsonResponse(query);
        var result = JsonConvert.DeserializeObject<ODataRecordEntity<T>>(jsonResponse, _jsonSettings);

        Guard.Against.Null(result, nameof(result));

        return result;
    }

    public async Task<ODataListEntity<T>> GetRecordsByQuery<T>(string query) where T : IEntity
    {
        var jsonResponse = await GetJsonResponse(query);
        var result = JsonConvert.DeserializeObject<ODataListEntity<T>>(jsonResponse, _jsonSettings);
        
        Guard.Against.Null(result, nameof(result));

        return result;
    }

    private async Task<string> GetJsonResponse(string query)
    {
        var httpResponse = await _httpClient.GetAsync(query);

        var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

        return jsonResponse;
    }
    
    public async Task<List<T>> GetRecords<T>(string query) where T : class, IEntity
    {
        var records = await _oDataClient.For<T>().Filter(query).FindEntriesAsync();

        return records.ToList();
    }
}