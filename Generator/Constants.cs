using System.Text.Encodings.Web;
using System.Text.Json;

namespace RPS.SADX.PopTracker.Generator;

internal static class Constants
{
    internal const string PackRoot = "../../../../pack";

    internal static readonly JsonSerializerOptions JsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true,
        IndentSize = 4
    };
}