using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Hamkare.Utility.Extensions;

public static class EnumExtensions
{
    public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct, Enum
    {
        if (Enum.TryParse<TEnum>(value.Replace("-",""), ignoreCase, out var result))
        {
            return result;
        }

        throw new ArgumentException($"Unable to convert '{value}' to enum type {typeof(TEnum).Name}");
    }

    public static string GetEnumDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null)
            return null;

        var attribute = field.GetCustomAttribute<DisplayAttribute>();
        return attribute != null ? attribute.GetName() : value.ToString();

    }
}