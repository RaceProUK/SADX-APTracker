namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class FileWriter
{
    internal static async Task WriteFile(string contents, string filename, params IEnumerable<string> pathSegments)
    {
        var folderPath = Path.Combine(["pack", .. pathSegments]);
        Directory.CreateDirectory(folderPath);

        using var file = File.OpenWrite(Path.Combine(folderPath, filename));
        using var writer = new StreamWriter(file);
        await writer.WriteAsync(contents);
    }
}