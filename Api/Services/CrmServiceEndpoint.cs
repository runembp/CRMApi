using CRMApi.Interfaces;
using CRMApi.Shared;
using Newtonsoft.Json;

namespace CRMApi.Services;

public class CrmServiceEndpoint(IApiClient apiClient) : ICrmServiceEndpoint
{
    private readonly HttpClient _httpClient = apiClient.GetClient();
    private readonly JsonSerializerSettings _jsonSettings = new()
    {
        MissingMemberHandling = MissingMemberHandling.Ignore
    };
    
    public async Task<T> GetRecordByQuery<T>(string query) where T : IEntity
    {
        var jsonResponse = await GetJsonResponse(query);
        
        var result = JsonConvert.DeserializeObject<ODataRecordEntity<T>>(jsonResponse, _jsonSettings);
        
        return result!.Entity;
    }

    public async Task<ODataListEntity<T>> GetRecordsByQuery<T>(string query) where T : IEntity
    {
        var jsonResponse = await GetJsonResponse(query);

        var result = JsonConvert.DeserializeObject<ODataListEntity<T>>(jsonResponse, _jsonSettings);

        return result!;
    }

    private async Task<string> GetJsonResponse(string query)
    {
        var httpResponse = await _httpClient.GetAsync(query);
        
        var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
        
        return jsonResponse;
    }
}