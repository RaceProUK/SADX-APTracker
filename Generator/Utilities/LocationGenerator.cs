using System.Collections.Frozen;
using System.Text.Json;
using System.Text.RegularExpressions;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int MissionsStart = 543800800;
    private const int MissionsEnd = 543800900;

    [GeneratedRegex("(\\d+)")]
    private static partial Regex NumberParser();

    internal static async Task Generate(Dictionary<string, int> dict)
    {
        var idToName = dict.ToFrozenDictionary(_ => _.Value, _ => _.Key);
        await GenerateLocationMapLua(idToName);
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

    private static async Task GenerateMissions(FrozenDictionary<int, string> idToName)
    {
        static int CalculateX(int number) => 56 * ((number - 1) % 10) + 38;
        static int CalculateY(int number) => 44 * ((number - 1) / 10) + 30;

        var missions = from entry in idToName
                       where entry.Key >= MissionsStart && entry.Key < MissionsEnd
                       let number = int.Parse(NumberParser().Match(entry.Value).Value)
                       let x = CalculateX(number)
                       let y = CalculateY(number)
                       select new Location(entry.Value,
                                           [new MapLocation("missions", x, y, 16, 1)],
                                           [new Section(Constants.MissionBriefs[number])]);
        await FileWriter.WriteFile(JsonSerializer.Serialize(missions, Constants.JsonOptions),
                                   "missions.json",
                                   "locations");
    }
}