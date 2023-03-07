namespace NETCoreDemo.Common;

using Microsoft.AspNetCore.WebUtilities;

public static class QueryStringExtension
{
    public static T? ParseParams<T>(this QueryString query) where T : new()
    {
        if (query.Value is null)
        {
            return default(T);
        }
        var queryParams = QueryHelpers.ParseQuery(query.Value);
        var instance = new T();

        // Using metadata (reflection)
        foreach (var property in typeof(T).GetProperties())
        {
            if (queryParams.TryGetValue(property.Name.ToLowerInvariant(), out var value))
            {
                // For the enum, we need to use Enum.Parse
                if (property.PropertyType.IsEnum)
                {
                    property.SetValue(instance, Enum.Parse(property.PropertyType, value.ToString()));
                }
                else
                {
                    property.SetValue(instance, Convert.ChangeType(value.ToString(), property.PropertyType));
                }
            }
        }
        return instance;
    }
}