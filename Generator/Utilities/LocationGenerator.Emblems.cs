using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int StationSquareEmblemsStart = 543800010;
    private const int StationSquareEmblemsEnd = 543800014;
    private const int MysticRuinsEmblemsStart = 543800020;
    private const int MysticRuinsEmblemsEnd = 543800024;
    private const int EggCarrierEmblemsStart = 543800030;
    private const int EggCarrierEmblemsEnd = 543800034;

    private static IEnumerable<LuaLocation> GenerateFieldEmblemsLua(FrozenDictionary<int, string> dict)
    {
        var ssEmblems = from entry in dict
                       where entry.Key >= StationSquareEmblemsStart && entry.Key < StationSquareEmblemsEnd
                       select new LuaLocation(entry.Key, "Station Square Field Emblems", entry.Value);
        var mrEmblems = from entry in dict
                       where entry.Key >= MysticRuinsEmblemsStart && entry.Key < MysticRuinsEmblemsEnd
                       select new LuaLocation(entry.Key, "Mystic Ruins Field Emblems", entry.Value);
        var ecEmblems = from entry in dict
                       where entry.Key >= EggCarrierEmblemsStart && entry.Key < EggCarrierEmblemsEnd
                       select new LuaLocation(entry.Key, "Egg Carrier Field Emblems", entry.Value);
        return ssEmblems.Union(mrEmblems).Union(ecEmblems);
    }

    private static async Task GenerateFieldEmblems(FrozenDictionary<int, string> dict)
    {
        var logic = await LogicLoader.LoadForFieldEmblem().ToListAsync();
        var ssEmblems = from entry in dict
                        where entry.Key >= StationSquareEmblemsStart && entry.Key < StationSquareEmblemsEnd
                        select new Section(entry.Value, AccessRules: GetAccessRules(entry.Value));
        var mrEmblems = from entry in dict
                        where entry.Key >= MysticRuinsEmblemsStart && entry.Key < MysticRuinsEmblemsEnd
                        select new Section(entry.Value, AccessRules: GetAccessRules(entry.Value));
        var ecEmblems = from entry in dict
                        where entry.Key >= EggCarrierEmblemsStart && entry.Key < EggCarrierEmblemsEnd
                        select new Section(entry.Value, AccessRules: GetAccessRules(entry.Value));
        var stationSquare = new Location("Station Square Field Emblems",
                                         [new MapLocation("levels", 1816, 640, LevelsIconSize, BorderThickness)],
                                         ssEmblems,
                                         VisibilityRules: ["FieldEmblemChecks"]);
        var mysticRuins = new Location("Mystic Ruins Field Emblems",
                                       [new MapLocation("levels", 1816, 900, LevelsIconSize, BorderThickness)],
                                       mrEmblems,
                                       VisibilityRules: ["FieldEmblemChecks"]);
        var eggCarrier = new Location("Egg Carrier Field Emblems",
                                      [new MapLocation("levels", 1816, 1160, LevelsIconSize, BorderThickness)],
                                      ecEmblems,
                                      VisibilityRules: ["FieldEmblemChecks"]);
        var emblems = new[] { stationSquare, mysticRuins, eggCarrier };
        await FileWriter.WriteFile(JsonSerializer.Serialize(emblems, Constants.JsonOptions),
                                   "emblems.json",
                                   "locations");

        IEnumerable<string>? GetAccessRules(string name) => logic.First(entry => entry.Name.Contains(name)).BuildAccessRules();
    }
}