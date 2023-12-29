namespace CRMApi.Utilities
{
    public static class Entity
    {
        public static IEnumerable<string> Properties<T>() where T : class
        {
            var properties = typeof(T).GetProperties();
            return properties.Select(property => property.Name);
        }
    }
    
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute(string logicalName) : Attribute
    {
        public string LogicalName { get; } = logicalName;
    }
}