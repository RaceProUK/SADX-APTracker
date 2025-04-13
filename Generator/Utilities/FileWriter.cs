namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class FileWriter
{
    internal static async Task WriteFile(string contents, string filename, params IEnumerable<string> pathSegments)
    {
        var folderPath = Path.Combine([Constants.PackRoot, .. pathSegments]);
        Directory.CreateDirectory(folderPath);

        using var file = File.Open(Path.Combine(folderPath, filename),
                                   FileMode.Create,
                                   FileAccess.Write,
                                   FileShare.Read);
        using var writer = new StreamWriter(file);
        await writer.WriteAsync(contents);
    }
}