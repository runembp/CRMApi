using System.Reflection;
using System.Text.Json.Serialization;

namespace CRMApi.Utilities
{
    public static class Entity
    {
        public static IEnumerable<string> Properties<T>() where T : class
        {
            var properties = typeof(T).GetProperties();
            return properties.Select(property => property.Name);
        }

        public static string[] JsonProperties<T>()
        {
            return typeof(T).GetProperties()
                .Select(p => p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name)
                .Where(name => !string.IsNullOrEmpty(name))
                .ToArray()!;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute(string logicalName) : Attribute
    {
        public string LogicalName { get; } = logicalName;
    }
}