using CRMApi.Shared;

namespace CRMApi.Interfaces;

public interface ICrmServiceEndpoint
{
    Task<T> GetRecordByQuery<T>(string query) where T : IEntity;
    Task<ODataListEntity<T>> GetRecordsByQuery<T>(string query) where T : IEntity;
}