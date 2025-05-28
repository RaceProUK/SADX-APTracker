using RPS.SADX.PopTracker.Generator.Models;
using System.Text.Json;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class DataPackageLoader
{
    internal static async Task<DataPackage?> LoadDataPackage()
    {
        var appdataLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dpLocation = Path.Combine(appdataLocal, "Archipelago", "Cache", "datapackage", "Sonic Adventure DX");
        var files = Directory.GetFiles(dpLocation);
        var dpFilePath = files.Length switch
        {
            1 => files[0],
            > 1 => ChooseFile(files),
            _ => throw new FileNotFoundException("Cannot find datapackage")
        };
        using var dpFile = File.OpenRead(dpFilePath);
        return await JsonSerializer.DeserializeAsync<DataPackage>(dpFile, Constants.JsonOptions);
    }

    private static string ChooseFile(string[] files)
    {
        while (true)
        {
            Console.WriteLine("Choose a datapackage to load:");
            for (var i = 0; i < files.Length; i++)
                Console.WriteLine($"{i} - {files[i]} (last updated {File.GetLastWriteTime(files[i])})");

            var input = Console.ReadLine();
            if (int.TryParse(input, out var id) && id >= 0 && id < files.Length)
                return files[id];
            else
            {
                Console.WriteLine("Invalid selection");
            }
        }
    }
}