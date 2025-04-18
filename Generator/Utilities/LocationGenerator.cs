using System.Collections.Frozen;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
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

    private const int StationSquareBossesStart = 543800700;
    private const int StationSquareBossesEnd = 543800730;
    private const int MysticRuinsBossesStart = 543800730;
    private const int MysticRuinsBossesEnd = 543800760;
    private const int EggCarrierBossesStart = 543800760;
    private const int EggCarrierBossesEnd = 543800800;

    private const int StationSquareEmblemsStart = 543800010;
    private const int StationSquareEmblemsEnd = 543800014;
    private const int MysticRuinsEmblemsStart = 543800020;
    private const int MysticRuinsEmblemsEnd = 543800024;
    private const int EggCarrierEmblemsStart = 543800030;
    private const int EggCarrierEmblemsEnd = 543800034;

    private const int MissionsStart = 543800800;
    private const int MissionsEnd = 543800900;

    private const int LevelsIconSize = 32;
    private const int MissionsIconSize = 16;
    private const int BorderThickness = 1;

    [GeneratedRegex("(\\d+)")]
    private static partial Regex NumberParser();

    [GeneratedRegex("(Mission [SABC])")]
    private static partial Regex MissionParser();

    internal static async Task Generate(Dictionary<string, int> dict)
    {
        var idToName = dict.ToFrozenDictionary(_ => _.Value, _ => _.Key);
        await GenerateLocationMapLua(idToName);
        await GenerateSubGames(idToName);
        await GenerateUpgrades(idToName);
        await GenerateBosses(idToName);
        await GenerateFieldEmblems(idToName);
        await GenerateMissions(idToName);
    }

    private static async Task GenerateLocationMapLua(FrozenDictionary<int, string> dict)
    {
        var entries = from entry in dict
                      orderby entry.Key
                      select $"    [{entry.Key}] = \"{entry.Value}\",";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["LocationMap = {", .. entries, "}"]),
                                   "locationMap.lua",
                                   "scripts",
                                   "archipelago");
    }

    private static async Task GenerateSubGames(FrozenDictionary<int, string> idToName)
    {
        var sc1Missions = from entry in idToName
                          where entry.Key >= SkyChaseAct1Start && entry.Key < SkyChaseAct1End
                          let mission = MissionParser().Match(entry.Value).Value
                          select new Section(mission);
        var sc2Missions = from entry in idToName
                          where entry.Key >= SkyChaseAct2Start && entry.Key < SkyChaseAct2End
                          let mission = MissionParser().Match(entry.Value).Value
                          select new Section(mission);
        var shMissions = from entry in idToName
                         where entry.Key >= SandHillStart && entry.Key < SandHillEnd
                         let mission = MissionParser().Match(entry.Value).Value
                         select new Section(mission);
        var tcMissions = from entry in idToName
                         where entry.Key >= TwinkleCircuitStart && entry.Key < TwinkleCircuitEnd
                         let mission = MissionParser().Match(entry.Value).Value
                         select new Section(mission);
        var skyChase1 = new Location("Sky Chase Act 1",
                                     [new MapLocation("levels", 1464, 1056, LevelsIconSize, BorderThickness)],
                                     sc1Missions);
        var skyChase2 = new Location("Sky Chase Act 2",
                                     [new MapLocation("levels", 1464, 1120, LevelsIconSize, BorderThickness)],
                                     sc2Missions);
        var sandHill = new Location("Sand Hill",
                                    [new MapLocation("levels", 1464, 1184, LevelsIconSize, BorderThickness)],
                                    shMissions);
        var twinkleCircuit = new Location("Twinkle Circuit",
                                          [new MapLocation("levels", 1464, 1248, LevelsIconSize, BorderThickness)],
                                          tcMissions);
        var subGames = new[] { skyChase1, skyChase2, sandHill, twinkleCircuit };
        await FileWriter.WriteFile(JsonSerializer.Serialize(subGames, Constants.JsonOptions),
                                   "subgames.json",
                                   "locations");
    }

    private static async Task GenerateUpgrades(FrozenDictionary<int, string> idToName)
    {
        var ssUpgradeIds = new[] { 543800100, 543800101, 543800200, 543800602 };
        var mrUpgradeIds = new[] { 543800102, 543800201, 543800300, 543800301, 543800600, 543800601, 543800603 };
        var ecUpgradeIds = new[] { 543800400, 543800401, 543800500, 543800501, 543800605 };
        var icUpgradeIds = new[] { 543800604 };
        var ssUpgrades = from entry in idToName
                         where ssUpgradeIds.Contains(entry.Key)
                         select new Section(entry.Value);
        var mrUpgrades = from entry in idToName
                         where mrUpgradeIds.Contains(entry.Key)
                         select new Section(entry.Value);
        var ecUpgrades = from entry in idToName
                         where ecUpgradeIds.Contains(entry.Key)
                         select new Section(entry.Value);
        var icUpgrades = from entry in idToName
                         where icUpgradeIds.Contains(entry.Key)
                         select new Section(entry.Value);
        var stationSquare = new Location("Upgrades",
                                         [new MapLocation("levels", 1744, 640, LevelsIconSize, BorderThickness)],
                                         ssUpgrades);
        var mysticRuins = new Location("Upgrades",
                                         [new MapLocation("levels", 1744, 900, LevelsIconSize, BorderThickness)],
                                         mrUpgrades);
        var eggCarrier = new Location("Upgrades",
                                      [new MapLocation("levels", 1744, 1160, LevelsIconSize, BorderThickness)],
                                      ecUpgrades);
        var iceCap = new Location("Upgrades",
                                  [new MapLocation("levels", 1962, 196, LevelsIconSize, BorderThickness)],
                                  icUpgrades);
        var upgrades = new[] { stationSquare, mysticRuins, eggCarrier, iceCap };
        await FileWriter.WriteFile(JsonSerializer.Serialize(upgrades, Constants.JsonOptions),
                                   "upgrades.json",
                                   "locations");
    }

    private static async Task GenerateBosses(FrozenDictionary<int, string> idToName)
    {
        static string TrimBossName(string name) => name.Replace(" Boss Fight", string.Empty);

        var ssBosses = from entry in idToName
                       where entry.Key >= StationSquareBossesStart && entry.Key < StationSquareBossesEnd
                       select new Section(TrimBossName(entry.Value));
        var mrBosses = from entry in idToName
                       where entry.Key >= MysticRuinsBossesStart && entry.Key < MysticRuinsBossesEnd
                       select new Section(TrimBossName(entry.Value));
        var ecBosses = from entry in idToName
                       where entry.Key >= EggCarrierBossesStart && entry.Key < EggCarrierBossesEnd
                       select new Section(TrimBossName(entry.Value));
        var stationSquare = new Location("Bosses",
                                         [new MapLocation("levels", 1792, 640, LevelsIconSize, BorderThickness)],
                                         ssBosses);
        var mysticRuins = new Location("Bosses",
                                       [new MapLocation("levels", 1792, 900, LevelsIconSize, BorderThickness)],
                                       mrBosses);
        var eggCarrier = new Location("Bosses",
                                      [new MapLocation("levels", 1792, 1160, LevelsIconSize, BorderThickness)],
                                      ecBosses);
        var bosses = new[] { stationSquare, mysticRuins, eggCarrier };
        await FileWriter.WriteFile(JsonSerializer.Serialize(bosses, Constants.JsonOptions),
                                   "bosses.json",
                                   "locations");
    }

    private static async Task GenerateFieldEmblems(FrozenDictionary<int, string> idToName)
    {
        var ssEmblems = from entry in idToName
                        where entry.Key >= StationSquareEmblemsStart && entry.Key < StationSquareEmblemsEnd
                        select new Section(entry.Value);
        var mrEmblems = from entry in idToName
                        where entry.Key >= MysticRuinsEmblemsStart && entry.Key < MysticRuinsEmblemsEnd
                        select new Section(entry.Value);
        var ecEmblems = from entry in idToName
                        where entry.Key >= EggCarrierEmblemsStart && entry.Key < EggCarrierEmblemsEnd
                        select new Section(entry.Value);
        var stationSquare = new Location("Field Emblems",
                                         [new MapLocation("levels", 1840, 640, LevelsIconSize, BorderThickness)],
                                         ssEmblems);
        var mysticRuins = new Location("Field Emblems",
                                       [new MapLocation("levels", 1840, 900, LevelsIconSize, BorderThickness)],
                                       mrEmblems);
        var eggCarrier = new Location("Field Emblems",
                                      [new MapLocation("levels", 1840, 1160, LevelsIconSize, BorderThickness)],
                                      ecEmblems);
        var emblems = new[] { stationSquare, mysticRuins, eggCarrier };
        await FileWriter.WriteFile(JsonSerializer.Serialize(emblems, Constants.JsonOptions),
                                   "emblems.json",
                                   "locations");
    }

    private static async Task GenerateMissions(FrozenDictionary<int, string> idToName)
    {
        static int CalculateX(int number) => 56 * ((number - 1) % 10) + 38;
        static int CalculateY(int number) => 44 * ((number - 1) / 10) + 30;

        var missions = from entry in idToName
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