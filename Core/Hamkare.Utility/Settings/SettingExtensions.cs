using System.Text.Encodings.Web;
using System.Text.Json;

namespace Hamkare.Utility.Settings;

public static class SettingExtensions
{
    public static string ConvertListObjectToStringSetting(this string objects)
    {
        var outPut = JsonSerializer.Serialize(objects, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
            PropertyNamingPolicy = null,
            DictionaryKeyPolicy = null
        });

        return outPut.Replace("\\\"", "\"").Trim('"');
    }
}