using System.Text.Json.Serialization;
using CRMApi.Interfaces;

namespace CRMApi.Shared;

public class ODataRecordEntity<T> where T : IEntity
{
    [JsonPropertyName("@odata.context")]
    public string? Context { get; init; }
    
    [JsonPropertyName("value")]
    public T? Entity { get; init; }
}