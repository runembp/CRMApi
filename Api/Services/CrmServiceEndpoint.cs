namespace CRMApi.Services;

public interface ICrmServiceEndpoint
{
    Task<List<string>> GetRecordsByQuery(string query);
}

public class CrmServiceEndpoint : ICrmServiceEndpoint
{
    public Task<List<string>> GetRecordsByQuery(string query)
    {
        return Task.FromResult(new List<string> { "Record 1", "Record 2" });
    }
}