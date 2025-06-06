﻿using System.Collections.Frozen;
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
        static string[] GetBossVisibility(string name, string playable) => name switch
        {
            string s when s.StartsWith("Egg Hornet") => [$"$NotHasItem|UnifyEggHornet,{playable}"],
            string s when s.StartsWith("Chaos 4") => [$"$NotHasItem|UnifyChaos4,{playable}"],
            string s when s.StartsWith("Chaos 6") => [$"$NotHasItem|UnifyChaos6,{playable}"],
            _ => [playable]
        };
        static string[]? GetSharedBossVisibility(string name) => name switch
        {
            "Egg Hornet" => ["UnifyEggHornet,SonicPlayable", "UnifyEggHornet,TailsPlayable"],
            "Chaos 4" => ["UnifyChaos4,SonicPlayable", "UnifyChaos4,TailsPlayable", "UnifyChaos4,KnucklesPlayable"],
            "Chaos 6" => ["UnifyChaos6,SonicPlayable", "UnifyChaos6,KnucklesPlayable", "UnifyChaos6,BigPlayable"],
            _ => default
        };
        static string[]? GetSharedBossAccess(string name) => name switch
        {
            "Egg Hornet" =>
            [
                "$CanReach|Sonic|MysticRuinsMain,PlayableSonic",
                "$CanReach|Tails|MysticRuinsMain,PlayableTails"
            ],
            "Chaos 4" =>
            [
                "$CanReach|Sonic|MysticRuinsMain,PlayableSonic",
                "$CanReach|Tails|MysticRuinsMain,PlayableTails",
                "$CanReach|Knuckles|MysticRuinsMain,PlayableKnuckles"
            ],
            "Chaos 6" =>
            [
                "$CanReach|Sonic|EggCarrierOutside,PlayableSonic",
                "$CanReach|Knuckles|EggCarrierOutside,PlayableKnuckles",
                "$CanReach|Big|EggCarrierOutside,PlayableBig"
            ],
            _ => default
        };
        static string GetStationSquareBossArea(string name) => name switch
        {
            string s when s.StartsWith("Chaos 2") => "Hotel",
            string s when s.StartsWith("Egg Walker") => "Casino",
            _ => "StationSquareMain"
        };
        static string GetMysticRuinsBossArea(string name) => name switch
        {
            string s when s.StartsWith("Egg Viper") || s.StartsWith("E-101 Beta") => "Jungle",
            _ => "MysticRuinsMain"
        };

        var ssBosses = from entry in dict
                       where entry.Key >= StationSquareBossesStart && entry.Key < StationSquareBossesEnd
                       let character = CharacterParser().Match(entry.Value).Groups[1].Value
                       let boss = TrimBossName(entry.Value)
                       let area = GetStationSquareBossArea(boss)
                       select new Section(boss,
                                          AccessRules: string.IsNullOrEmpty(character)
                                          ? GetSharedBossAccess(boss)
                                          : [$"$CanReach|{character}|{area},Playable{character}"],
                                          VisibilityRules: string.IsNullOrEmpty(character)
                                          ? GetSharedBossVisibility(boss)
                                          : GetBossVisibility(boss, $"{character}Playable"));
        var mrBosses = from entry in dict
                       where entry.Key >= MysticRuinsBossesStart && entry.Key < MysticRuinsBossesEnd
                       let character = CharacterParser().Match(entry.Value).Groups[1].Value
                       let boss = TrimBossName(entry.Value)
                       let area = GetMysticRuinsBossArea(boss)
                       select new Section(boss,
                                          AccessRules: string.IsNullOrEmpty(character)
                                          ? GetSharedBossAccess(boss)
                                          : [$"$CanReach|{character}|{area},Playable{character}"],
                                          VisibilityRules: string.IsNullOrEmpty(character)
                                          ? GetSharedBossVisibility(boss)
                                          : GetBossVisibility(boss, $"{character}Playable"));
        var ecBosses = from entry in dict
                       where entry.Key >= EggCarrierBossesStart && entry.Key < EggCarrierBossesEnd
                       let character = CharacterParser().Match(entry.Value).Groups[1].Value
                       let boss = TrimBossName(entry.Value)
                       select new Section(boss,
                                          AccessRules: string.IsNullOrEmpty(character)
                                          ? GetSharedBossAccess(boss)
                                          : [$"$CanReach|{character}|EggCarrierOutside,Playable{character}"],
                                          VisibilityRules: string.IsNullOrEmpty(character)
                                          ? GetSharedBossVisibility(boss)
                                          : GetBossVisibility(boss, $"{character}Playable"));
        var stationSquare = new Location("Station Square Bosses",
                                         [new MapLocation("levels", 1768, 640, LevelsIconSize, BorderThickness)],
                                         ssBosses);
        var mysticRuins = new Location("Mystic Ruins Bosses",
                                       [new MapLocation("levels", 1768, 900, LevelsIconSize, BorderThickness)],
                                       mrBosses);
        var eggCarrier = new Location("Egg Carrier Bosses",
                                      [new MapLocation("levels", 1768, 1160, LevelsIconSize, BorderThickness)],
                                      ecBosses);
        var perfectChaos = new Location("Perfect Chaos",
                                        [new MapLocation("levels", 1792, 598, LevelsIconSize, BorderThickness)],
                                        [new Section("Open Their Heart")],
                                        AccessRules: ["$HasMetGoalCriteria"]);
        var bosses = new[] { stationSquare, mysticRuins, eggCarrier, perfectChaos };
        await FileWriter.WriteFile(JsonSerializer.Serialize(bosses, Constants.JsonOptions),
                                   "bosses.json",
                                   "locations");
    }
}