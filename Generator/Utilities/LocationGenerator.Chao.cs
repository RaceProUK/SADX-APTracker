using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int GoldEgg = 543800900;
    private const int SilverEgg = 543800901;
    private const int BlackEgg = 543800902;
    private const int RacesStart = 543800905;
    private const int RacesEnd = 543800910;

    private static async Task GenerateChaoEggs(FrozenDictionary<int, string> dict)
    {
        var ssUpgrades = from entry in dict
                         where entry.Key == GoldEgg
                         select new Section(entry.Value);
        var mrUpgrades = from entry in dict
                         where entry.Key == SilverEgg
                         select new Section(entry.Value);
        var ecUpgrades = from entry in dict
                         where entry.Key == BlackEgg
                         select new Section(entry.Value);
        var stationSquare = new Location("Station Square Chao Egg",
                                         [new MapLocation("levels", 1864, 640, LevelsIconSize, BorderThickness)],
                                         ssUpgrades,
                                         VisibilityRules: ["SecretChaoEggs"]);
        var mysticRuins = new Location("Mystic Ruins Chao Egg",
                                       [new MapLocation("levels", 1864, 900, LevelsIconSize, BorderThickness)],
                                       mrUpgrades,
                                       VisibilityRules: ["SecretChaoEggs"]);
        var eggCarrier = new Location("Egg Carrier Chao Egg",
                                      [new MapLocation("levels", 1864, 1160, LevelsIconSize, BorderThickness)],
                                      ecUpgrades,
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
                                     VisibilityRules: ["ChaoRacesRequired"]);
        await FileWriter.WriteFile(JsonSerializer.Serialize(new[] { chaoRaces }, Constants.JsonOptions),
                                   "chaoRaces.json",
                                   "locations");
    }
}