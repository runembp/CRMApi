namespace CRMApi.Shared;

using System;

[AttributeUsage(AttributeTargets.Class)]
public class EntityAttribute(string logicalName) : Attribute
{
    public string LogicalName { get; } = logicalName;
}