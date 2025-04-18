using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int MissionsStart = 543800800;
    private const int MissionsEnd = 543800900;

    private static async Task GenerateMissions(FrozenDictionary<int, string> dict)
    {
        static int CalculateX(int number) => 56 * ((number - 1) % 10) + 38;
        static int CalculateY(int number) => 44 * ((number - 1) / 10) + 30;

        var missions = from entry in dict
                       where entry.Key >= MissionsStart && entry.Key < MissionsEnd
                       let number = int.Parse(NumberParser().Match(entry.Value).Value)
                       let x = CalculateX(number)
                       let y = CalculateY(number)
                       select new Location(entry.Value,
                                           [new MapLocation("missions", x, y, MissionsIconSize, BorderThickness)],
                                           [new Section(Constants.MissionBriefs[number])]);
        await FileWriter.WriteFile(JsonSerializer.Serialize(missions, Constants.JsonOptions),
                                   "missions.json",
                                   "locations");
    }
}