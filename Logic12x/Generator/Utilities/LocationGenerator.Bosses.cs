using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int PerfectChaos = 543800009;
    private const int StationSquareBossesStart = 543800700;
    private const int StationSquareBossesEnd = 543800730;
    private const int MysticRuinsBossesStart = 543800730;
    private const int MysticRuinsBossesEnd = 543800770;
    private const int EggCarrierBossesStart = 543800770;
    private const int EggCarrierBossesEnd = 543800800;

    private const string BossesMap = "levels";

    private static string TrimBossName(string name) => name.Replace(" Boss Fight", string.Empty);

    private static IEnumerable<LuaLocation> GenerateBossesLua(FrozenDictionary<int, string> dict)
    {
        var perfectChaos = new LuaLocation(PerfectChaos, "Perfect Chaos", "Open Their Heart");
        var ssBosses = from entry in dict
                       where entry.Key >= StationSquareBossesStart && entry.Key < StationSquareBossesEnd
                       let boss = TrimBossName(entry.Value)
                       select new LuaLocation(entry.Key, "Station Square Bosses", boss);
        var mrBosses = from entry in dict
                       where entry.Key >= MysticRuinsBossesStart && entry.Key < MysticRuinsBossesEnd
                       let boss = TrimBossName(entry.Value)
                       select new LuaLocation(entry.Key, "Mystic Ruins Bosses", boss);
        var ecBosses = from entry in dict
                       where entry.Key >= EggCarrierBossesStart && entry.Key < EggCarrierBossesEnd
                       let boss = TrimBossName(entry.Value)
                       select new LuaLocation(entry.Key, "Egg Carrier Bosses", boss);
        return [perfectChaos, .. ssBosses.Union(mrBosses).Union(ecBosses)];
    }

    private static async Task GenerateBosses(FrozenDictionary<int, string> dict)
    {
        var ssBosses = from entry in dict
                       where entry.Key >= StationSquareBossesStart && entry.Key < StationSquareBossesEnd
                       let boss = TrimBossName(entry.Value)
                       select new Section(boss);
        var mrBosses = from entry in dict
                       where entry.Key >= MysticRuinsBossesStart && entry.Key < MysticRuinsBossesEnd
                       let boss = TrimBossName(entry.Value)
                       select new Section(boss);
        var ecBosses = from entry in dict
                       where entry.Key >= EggCarrierBossesStart && entry.Key < EggCarrierBossesEnd
                       let boss = TrimBossName(entry.Value)
                       select new Section(boss);
        var stationSquare = new Location("Station Square Bosses",
                                         [new MapLocation(BossesMap, 1768, 640, LevelsIconSize, BorderThickness)],
                                         ssBosses);
        var mysticRuins = new Location("Mystic Ruins Bosses",
                                       [new MapLocation(BossesMap, 1768, 900, LevelsIconSize, BorderThickness)],
                                       mrBosses);
        var eggCarrier = new Location("Egg Carrier Bosses",
                                      [new MapLocation(BossesMap, 1768, 1160, LevelsIconSize, BorderThickness)],
                                      ecBosses);
        var perfectChaos = new Location("Perfect Chaos",
                                        [new MapLocation(BossesMap, 1792, 598, LevelsIconSize, BorderThickness)],
                                        [new Section("Open Their Heart")]);
        var bosses = new[] { stationSquare, mysticRuins, eggCarrier, perfectChaos };
        await FileWriter.WriteFile(JsonSerializer.Serialize(bosses, Constants.JsonOptions),
                                   "bosses.json",
                                   "locations");
    }
}