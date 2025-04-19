using System.Collections.Frozen;
using System.Text.Json;
using System.Text.RegularExpressions;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int LevelsIconSize = 32;
    private const int MissionsIconSize = 16;
    private const int BorderThickness = 1;

    [GeneratedRegex("\\(([A-Za-z]+)\\)")]
    private static partial Regex CharacterParser();

    [GeneratedRegex("(\\d+)")]
    private static partial Regex NumberParser();

    [GeneratedRegex("Sub-Level - ([A-Za-z ]+)")]
    private static partial Regex SublevelParser();

    internal static async Task Generate(IDictionary<string, int> dict)
    {
        var idToName = dict.ToFrozenDictionary(_ => _.Value, _ => _.Key);
        await GenerateLocationMapLua(idToName);
        await GenerateLevels(idToName);
        await GenerateSubGames(idToName);
        await GenerateUpgrades(idToName);
        await GenerateBosses(idToName);
        await GenerateFieldEmblems(idToName);
        await GenerateChaoEggs(idToName);
        await GenerateChaoRaces(idToName);
        await GenerateEnemies(idToName);
        await GenerateCapsules(idToName);
        await GenerateMissions(idToName);
    }

    private static async Task GenerateLocationMapLua(FrozenDictionary<int, string> dict)
    {
        var entries = from entry in dict
                      orderby entry.Key
                      select $"    [{entry.Key}] = \"{entry.Value}\",";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["LocationMap = {", .. entries, "}"]),
                                   "locationMap.lua",
                                   "scripts",
                                   "archipelago");
    }
}