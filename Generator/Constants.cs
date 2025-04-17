using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator;

internal static class Constants
{
    internal const string PackRoot = "../../../../pack";

    internal static readonly JsonSerializerOptions JsonOptions = new()
    {
        Converters = { new ItemConverter() },
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true,
        IndentSize = 4
    };

    private sealed class ItemConverter : JsonConverter<Item>
    {
        public override Item? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotImplementedException();

        public override void Write(Utf8JsonWriter writer, Item value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case ToggleItem toggle:
                    JsonSerializer.Serialize(writer, toggle, options);
                    break;

                case CollectibleItem collectible:
                    JsonSerializer.Serialize(writer, collectible, options);
                    break;

                case ProgressiveItem progressive:
                    JsonSerializer.Serialize(writer, progressive, options);
                    break;

                default:
                    JsonSerializer.Serialize(writer, value, options);
                    break;
            }
        }
    }
}