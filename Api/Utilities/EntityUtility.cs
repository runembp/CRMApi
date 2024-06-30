using System.Reflection;

namespace CRMApi.Utilities
{
    public static class EntityUtility
    {
        public static string[] Properties<T>()
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