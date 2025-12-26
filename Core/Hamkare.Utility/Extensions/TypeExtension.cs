using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;

namespace Hamkare.Utility.Extensions;

public static class TypeExtension
{
    public static string GetPropertyDisplayName(this Type objectType, string propertyName)
    {
        var propertyInfo = objectType.GetProperty(propertyName);
        if (propertyInfo == null)
            return propertyName;

        var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
        return displayAttribute != null ? displayAttribute.Name : propertyName;
    }

    public static string GetClassDisplayName(this Type objectType)
    {
        var displayAttribute = objectType.GetCustomAttribute<DisplayAttribute>();
        return displayAttribute != null ? displayAttribute.Name : objectType.Name;
    }

    public static T Clone<T>(this T source)
    {
        if (source == null)
            return default;

        var json = JsonSerializer.Serialize(source);
        return JsonSerializer.Deserialize<T>(json);
    }
}