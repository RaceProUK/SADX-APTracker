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
        static string[] GetBossVisibility(string name, string playable) => name switch
        {
            string s when s.StartsWith("Egg Hornet") => [$"$InvertItem|UnifyEggHornet,{playable}"],
            string s when s.StartsWith("Chaos 4") => [$"$InvertItem|UnifyChaos4,{playable}"],
            string s when s.StartsWith("Chaos 6") => [$"$InvertItem|UnifyChaos6,{playable}"],
            _ => [playable]
        };
        static string[]? GetSharedBossVisibility(string name) => name switch
        {
            "Egg Hornet" => ["UnifyEggHornet,SonicPlayable", "UnifyEggHornet,TailsPlayable"],
            "Chaos 4" => ["UnifyChaos4,SonicPlayable", "UnifyChaos4,TailsPlayable", "UnifyChaos4,KnucklesPlayable"],
            "Chaos 6" => ["UnifyChaos6,SonicPlayable", "UnifyChaos6,KnucklesPlayable", "UnifyChaos6,BigPlayable"],
            _ => default
        };

        var ssBosses = from entry in dict
                       where entry.Key >= StationSquareBossesStart && entry.Key < StationSquareBossesEnd
                       let character = CharacterParser().Match(entry.Value).Groups[1].Value
                       let boss = TrimBossName(entry.Value)
                       select new Section(boss,
                                          VisibilityRules: string.IsNullOrEmpty(character)
                                          ? GetSharedBossVisibility(boss)
                                          : GetBossVisibility(boss, $"{character}Playable"));
        var mrBosses = from entry in dict
                       where entry.Key >= MysticRuinsBossesStart && entry.Key < MysticRuinsBossesEnd
                       let character = CharacterParser().Match(entry.Value).Groups[1].Value
                       let boss = TrimBossName(entry.Value)
                       select new Section(boss,
                                          VisibilityRules: string.IsNullOrEmpty(character)
                                          ? GetSharedBossVisibility(boss)
                                          : GetBossVisibility(boss, $"{character}Playable"));
        var ecBosses = from entry in dict
                       where entry.Key >= EggCarrierBossesStart && entry.Key < EggCarrierBossesEnd
                       let character = CharacterParser().Match(entry.Value).Groups[1].Value
                       let boss = TrimBossName(entry.Value)
                       select new Section(boss,
                                          VisibilityRules: string.IsNullOrEmpty(character)
                                          ? GetSharedBossVisibility(boss)
                                          : GetBossVisibility(boss, $"{character}Playable"));
        var stationSquare = new Location("Siation Square Bosses",
                                         [new MapLocation("levels", 1768, 640, LevelsIconSize, BorderThickness)],
                                         [.. ssBosses, new Section("Perfect Chaos")]);
        var mysticRuins = new Location("Mystic Ruins Bosses",
                                       [new MapLocation("levels", 1768, 900, LevelsIconSize, BorderThickness)],
                                       mrBosses);
        var eggCarrier = new Location("Egg Carrier Bosses",
                                      [new MapLocation("levels", 1768, 1160, LevelsIconSize, BorderThickness)],
                                      ecBosses);
        var bosses = new[] { stationSquare, mysticRuins, eggCarrier };
        await FileWriter.WriteFile(JsonSerializer.Serialize(bosses, Constants.JsonOptions),
                                   "bosses.json",
                                   "locations");
    }
}