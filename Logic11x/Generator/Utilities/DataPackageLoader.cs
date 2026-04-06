using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class DataPackageLoader
{
    internal static async Task<DataPackage?> LoadDataPackage()
    {
        var appdataLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dpFilePath = Path.Combine(appdataLocal, "Archipelago", "Cache", "datapackage", "Sonic Adventure DX", "7d5f6018d7efb5811273ecc815fe29de274f5eb1.json");
        using var dpFile = File.OpenRead(dpFilePath);
        return await JsonSerializer.DeserializeAsync<DataPackage>(dpFile, Constants.JsonOptions);
    }
}