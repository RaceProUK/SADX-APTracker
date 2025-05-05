using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int GoldEgg = 543800900;
    private const int SilverEgg = 543800901;
    private const int BlackEgg = 543800902;
    private const int RacesStart = 543800905;
    private const int RacesEnd = 543800910;

    private static IEnumerable<LuaLocation> GenerateChaoEggsLua(FrozenDictionary<int, string> dict)
    {
        yield return new LuaLocation(GoldEgg, "Station Square Chao Egg", dict[GoldEgg]);
        yield return new LuaLocation(SilverEgg, "Mystic Ruins Chao Egg", dict[SilverEgg]);
        yield return new LuaLocation(BlackEgg, "Egg Carrier Chao Egg", dict[BlackEgg]);
    }

    private static IEnumerable<LuaLocation> GenerateChaoRacesLua(FrozenDictionary<int, string> dict)
        => from entry in dict
           where entry.Key >= RacesStart && entry.Key < RacesEnd
           select new LuaLocation(entry.Key, "Chao Races", entry.Value);

    private static async Task GenerateChaoEggs(FrozenDictionary<int, string> dict)
    {
        var stationSquare = new Location("Station Square Chao Egg",
                                         [new MapLocation("levels", 1864, 640, LevelsIconSize, BorderThickness)],
                                         [(new Section(dict[GoldEgg]))],
                                         AccessRules:
                                         [
                                             .. AccessRulesGenerator.Characters.Select(_ => $"$CanReach|{_}|StationSquareMain,HotelFrontKey,Playable{_}"),
                                             .. AccessRulesGenerator.Characters.Select(_ => $"$CanReach|{_}|StationSquareMain,StationBackKey,StationFrontKey,HotelBackKey,Playable{_}")
                                         ],
                                         VisibilityRules: ["SecretChaoEggs"]);
        var mysticRuins = new Location("Mystic Ruins Chao Egg",
                                       [new MapLocation("levels", 1864, 900, LevelsIconSize, BorderThickness)],
                                       [(new Section(dict[SilverEgg]))],
                                       AccessRules: AccessRulesGenerator.Characters.Select(_ => $"$CanReach|{_}|MysticRuinsMain,Playable{_}"),
                                       VisibilityRules: ["SecretChaoEggs"]);
        var eggCarrier = new Location("Egg Carrier Chao Egg",
                                      [new MapLocation("levels", 1864, 1160, LevelsIconSize, BorderThickness)],
                                      [(new Section(dict[BlackEgg]))],
                                      AccessRules: AccessRulesGenerator.Characters.Select(_ => $"$CanReach|{_}|EggCarrierInside,Playable{_}"),
                                      VisibilityRules: ["SecretChaoEggs"]);
        var chaoEggs = new[] { stationSquare, mysticRuins, eggCarrier };
        await FileWriter.WriteFile(JsonSerializer.Serialize(chaoEggs, Constants.JsonOptions),
                                   "chaoEggs.json",
                                   "locations");
    }

    private static async Task GenerateChaoRaces(FrozenDictionary<int, string> dict)
    {
        var chaoRaces = new Location("Chao Races",
                                     [new MapLocation("levels", 1912, 640, LevelsIconSize, BorderThickness)],
                                     from entry in dict
                                     where entry.Key >= RacesStart && entry.Key < RacesEnd
                                     select new Section(entry.Value),
                                     AccessRules: AccessRulesGenerator.Characters.Select(_ => $"$CanReach|{_}|Hotel,Playable{_}"),
                                     VisibilityRules: ["ChaoRacesRequired"]);
        await FileWriter.WriteFile(JsonSerializer.Serialize(new[] { chaoRaces }, Constants.JsonOptions),
                                   "chaoRaces.json",
                                   "locations");
    }
}