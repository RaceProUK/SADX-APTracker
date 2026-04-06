using RPS.SADX.PopTracker.Generator.Models;
using System.Text.Json;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class DataPackageLoader
{
    internal static async Task<DataPackage?> LoadDataPackage()
    {
        var appdataLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dpFilePath = Path.Combine(appdataLocal, "Archipelago", "Cache", "datapackage", "Sonic Adventure DX", "d8a37df8f9549c1633fe4a9d14cb8130ac7c221e.json");
        using var dpFile = File.OpenRead(dpFilePath);
        return await JsonSerializer.DeserializeAsync<DataPackage>(dpFile, Constants.JsonOptions);
    }
}