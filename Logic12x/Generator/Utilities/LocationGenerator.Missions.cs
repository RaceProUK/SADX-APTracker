using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int MissionsStart = 543800800;
    private const int MissionsEnd = 543800900;

    private const int StartX = 56;
    private const int StartY = 44;
    private const int OffsetX = 38;
    private const int OffsetY = 30;
    private const int LineLength = 10;

    private static IEnumerable<LuaLocation> GenerateMissionsLua(FrozenDictionary<int, string> dict)
        => from entry in dict
           where entry.Key >= MissionsStart && entry.Key < MissionsEnd
           let number = int.Parse(NumberParser().Match(entry.Value).Value)
           select new LuaLocation(entry.Key, entry.Value, Constants.MissionBriefs[number]);

    private static async Task GenerateMissions(FrozenDictionary<int, string> dict)
    {
        static int CalculateX(int number) => StartX * ((number - 1) % LineLength) + OffsetX;
        static int CalculateY(int number) => StartY * ((number - 1) / LineLength) + OffsetY;

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