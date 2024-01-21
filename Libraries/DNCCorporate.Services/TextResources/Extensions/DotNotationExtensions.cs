using System.Text.Json;

namespace DNCCorporate.Services;

public static class DotNotationExtensions
{
    public static Dictionary<string, string?> FromJsonObject(this Dictionary<string, JsonElement> jsonObj)
    {
        ArgumentNullException.ThrowIfNull(jsonObj, nameof(jsonObj));

        var dotNotation = new Dictionary<string, string?>();

        foreach (var obj in jsonObj)
        {
            if (obj.Value.ValueKind == JsonValueKind.Object)
            {
                var nested = obj.Value.Deserialize<Dictionary<string, JsonElement>>();
                if(nested == null)
                {
                    throw new DotNotationJsonValueException($"Error in json object value deserialization. Value {obj.Value}");
                }
                var nestedDotNotation = FromJsonObject(nested);

                foreach (var nestedObj in nestedDotNotation)
                {
                    dotNotation.Add($"{obj.Key}.{nestedObj.Key}", nestedObj.Value);
                }
            }
            else if (obj.Value.ValueKind == JsonValueKind.String)
            {
                dotNotation.Add(obj.Key, obj.Value.GetString() ?? string.Empty);
            }
            else if (obj.Value.ValueKind == JsonValueKind.Null)
            {
                dotNotation.Add(obj.Key, null);
            }
        }

        return dotNotation;
    }
}
