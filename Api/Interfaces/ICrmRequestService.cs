using CRMApi.Entities;

namespace CRMApi.Interfaces;

public interface ICrmRequestService
{
    Task<ODataRecordEntity<T>> GetRecordByQuery<T>(string query) where T : IEntity;
    Task<ODataListEntity<T>> GetRecordsByQuery<T>(string query) where T : IEntity;
    Task<List<T>> GetRecords<T>(string query) where T : class, IEntity;
}