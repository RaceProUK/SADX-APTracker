using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int StationSquareBossesStart = 543800700;
    private const int StationSquareBossesEnd = 543800730;
    private const int MysticRuinsBossesStart = 543800730;
    private const int MysticRuinsBossesEnd = 543800760;
    private const int EggCarrierBossesStart = 543800760;
    private const int EggCarrierBossesEnd = 543800800;

    private static async Task GenerateBosses(FrozenDictionary<int, string> dict)
    {
        static string TrimBossName(string name) => name.Replace(" Boss Fight", string.Empty);

        var ssBosses = from entry in dict
                       where entry.Key >= StationSquareBossesStart && entry.Key < StationSquareBossesEnd
                       select new Section(TrimBossName(entry.Value));
        var mrBosses = from entry in dict
                       where entry.Key >= MysticRuinsBossesStart && entry.Key < MysticRuinsBossesEnd
                       select new Section(TrimBossName(entry.Value));
        var ecBosses = from entry in dict
                       where entry.Key >= EggCarrierBossesStart && entry.Key < EggCarrierBossesEnd
                       select new Section(TrimBossName(entry.Value));
        var stationSquare = new Location("Bosses",
                                         [new MapLocation("levels", 1768, 640, LevelsIconSize, BorderThickness)],
                                         [.. ssBosses, new Section("Perfect Chaos")]);
        var mysticRuins = new Location("Bosses",
                                       [new MapLocation("levels", 1768, 900, LevelsIconSize, BorderThickness)],
                                       mrBosses);
        var eggCarrier = new Location("Bosses",
                                      [new MapLocation("levels", 1768, 1160, LevelsIconSize, BorderThickness)],
                                      ecBosses);
        var bosses = new[] { stationSquare, mysticRuins, eggCarrier };
        await FileWriter.WriteFile(JsonSerializer.Serialize(bosses, Constants.JsonOptions),
                                   "bosses.json",
                                   "locations");
    }
}