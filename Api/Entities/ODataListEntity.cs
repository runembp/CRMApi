using CRMApi.Interfaces;
using Newtonsoft.Json;

namespace CRMApi.Entities;

public class ODataListEntity<T> where T : IEntity
{
    [JsonProperty("@odata.context")]
    public string? ODataContext { get; init; }
    
    [JsonProperty("value")]
    public List<T>? Entities { get; init; }
}