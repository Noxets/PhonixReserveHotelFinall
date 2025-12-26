using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Hamkare.Utility.Extensions;

public static class AttributeExtension
{
    public static string GetDisplayName(this PropertyInfo propertyInfo)
    {
        var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
        return displayAttribute?.Name ?? propertyInfo.Name;
    }
}