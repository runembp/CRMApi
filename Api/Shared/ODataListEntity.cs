using CRMApi.Interfaces;
using Newtonsoft.Json;

namespace CRMApi.Shared;

public class ODataListEntity<T> where T : IEntity
{
    [JsonProperty("@odata.context")]
    public string ODataContext { get; set; }
    
    [JsonProperty("value")]
    public List<T> Entities { get; set; }
}