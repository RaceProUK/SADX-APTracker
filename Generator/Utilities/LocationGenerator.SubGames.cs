using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int TwinkleCircuitStart = 543800015;
    private const int TwinkleCircuitEnd = 543800017;
    private const int SandHillStart = 543800025;
    private const int SandHillEnd = 543800027;
    private const int SkyChaseAct1Start = 543800027;
    private const int SkyChaseAct1End = 543800029;
    private const int SkyChaseAct2Start = 543800035;
    private const int SkyChaseAct2End = 543800037;

    private static async Task GenerateSubGames(FrozenDictionary<int, string> dict)
    {
        var sc1Missions = from entry in dict
                          where entry.Key >= SkyChaseAct1Start && entry.Key < SkyChaseAct1End
                          let mission = MissionParser().Match(entry.Value).Value
                          select new Section(mission);
        var sc2Missions = from entry in dict
                          where entry.Key >= SkyChaseAct2Start && entry.Key < SkyChaseAct2End
                          let mission = MissionParser().Match(entry.Value).Value
                          select new Section(mission);
        var shMissions = from entry in dict
                         where entry.Key >= SandHillStart && entry.Key < SandHillEnd
                         let mission = MissionParser().Match(entry.Value).Value
                         select new Section(mission);
        var tcMissions = from entry in dict
                         where entry.Key >= TwinkleCircuitStart && entry.Key < TwinkleCircuitEnd
                         let mission = MissionParser().Match(entry.Value).Value
                         select new Section(mission);
        var skyChase1 = new Location("Sky Chase Act 1",
                                     [new MapLocation("levels", 1082, 1056, LevelsIconSize, BorderThickness)],
                                     sc1Missions,
                                     VisibilityRules: ["SonicPlayable", "TailsPlayable"]);
        var skyChase2 = new Location("Sky Chase Act 2",
                                     [new MapLocation("levels", 1082, 1120, LevelsIconSize, BorderThickness)],
                                     sc2Missions,
                                     VisibilityRules: ["SonicPlayable", "TailsPlayable"]);
        var sandHill = new Location("Sand Hill",
                                    [new MapLocation("levels", 1082, 1184, LevelsIconSize, BorderThickness)],
                                    shMissions,
                                    VisibilityRules: ["SonicPlayable", "TailsPlayable"]);
        var twinkleCircuit = new Location("Twinkle Circuit",
                                          [new MapLocation("levels", 1082, 1248, LevelsIconSize, BorderThickness)],
                                          tcMissions);
        var subGames = new[] { skyChase1, skyChase2, sandHill, twinkleCircuit };
        await FileWriter.WriteFile(JsonSerializer.Serialize(subGames, Constants.JsonOptions),
                                   "subgames.json",
                                   "locations");
    }
}