using System.Collections.Frozen;
using System.Text.Json;
using Humanizer;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int LightShoes = 543800100;
    private const int CrystalRing = 543800101;
    private const int AncientLight = 543800102;
    private const int JetAnklet = 543800200;
    private const int RhythmBadge = 543800201;
    private const int ShovelClaw = 543800300;
    private const int FightingGloves = 543800301;
    private const int WarriorFeather = 543800400;
    private const int LongHammer = 543800401;
    private const int JetBooster = 543800500;
    private const int LaserBlaster = 543800501;
    private const int LifeBelt = 543800600;
    private const int PowerRod = 543800601;
    private const int Lure1 = 543800602;
    private const int Lure2 = 543800603;
    private const int Lure3 = 543800604;
    private const int Lure4 = 543800605;

    private static readonly int[] ssUpgradeIds = [LightShoes, CrystalRing, JetAnklet, Lure1];
    private static readonly int[] mrUpgradeIds = [AncientLight, RhythmBadge, ShovelClaw, FightingGloves, LifeBelt, PowerRod, Lure2];
    private static readonly int[] ecUpgradeIds = [WarriorFeather, LongHammer, JetBooster, LaserBlaster, Lure4];
    private static readonly int[] icUpgradeIds = [Lure3];

    private static IEnumerable<LuaLocation> GenerateUpgradesLua(FrozenDictionary<int, string> dict)
    {
        var ssUpgrades = from entry in dict
                         where ssUpgradeIds.Contains(entry.Key)
                         select new LuaLocation(entry.Key, "Station Square Upgrades", entry.Value);
        var mrUpgrades = from entry in dict
                         where mrUpgradeIds.Contains(entry.Key)
                         select new LuaLocation(entry.Key, "Mystic Ruins Upgrades", entry.Value);
        var ecUpgrades = from entry in dict
                         where ecUpgradeIds.Contains(entry.Key)
                         select new LuaLocation(entry.Key, "Egg Carrier Upgrades", entry.Value);
        var icUpgrades = from entry in dict
                         where icUpgradeIds.Contains(entry.Key)
                         select new LuaLocation(entry.Key, "Ice Cap Upgrade", entry.Value);
        return ssUpgrades.Union(mrUpgrades).Union(ecUpgrades).Union(icUpgrades);
    }

    private static async Task GenerateUpgrades(FrozenDictionary<int, string> dict)
    {
        static string GetUpgradeCharacter(int number) => number switch
        {
            LightShoes or CrystalRing or AncientLight => "Sonic",
            JetAnklet or RhythmBadge => "Tails",
            ShovelClaw or FightingGloves => "Knuckles",
            WarriorFeather or LongHammer => "Amy",
            JetBooster or LaserBlaster => "Gamma",
            _ => "Big"
        };

        var logic = await LogicLoader.LoadForUpgradeItem().ToListAsync();
        var ssUpgrades = from entry in dict
                         where ssUpgradeIds.Contains(entry.Key)
                         select new Section(entry.Value,
                                            AccessRules: GetAccessRules(entry.Value),
                                            VisibilityRules: [$"{GetUpgradeCharacter(entry.Key)}Playable"]);
        var mrUpgrades = from entry in dict
                         where mrUpgradeIds.Contains(entry.Key)
                         select new Section(entry.Value,
                                            AccessRules: GetAccessRules(entry.Value),
                                            VisibilityRules: [$"{GetUpgradeCharacter(entry.Key)}Playable"]);
        var ecUpgrades = from entry in dict
                         where ecUpgradeIds.Contains(entry.Key)
                         select new Section(entry.Value,
                                            AccessRules: GetAccessRules(entry.Value),
                                            VisibilityRules: [$"{GetUpgradeCharacter(entry.Key)}Playable"]);
        var icUpgrades = from entry in dict
                         where icUpgradeIds.Contains(entry.Key)
                         select new Section(entry.Value,
                                            AccessRules: GetAccessRules(entry.Value),
                                            VisibilityRules: [$"{GetUpgradeCharacter(entry.Key)}Playable"]);
        var stationSquare = new Location("Station Square Upgrades",
                                         [new MapLocation("levels", 1722, 640, LevelsIconSize, BorderThickness)],
                                         ssUpgrades);
        var mysticRuins = new Location("Mystic Ruins Upgrades",
                                       [new MapLocation("levels", 1722, 900, LevelsIconSize, BorderThickness)],
                                       mrUpgrades);
        var eggCarrier = new Location("Egg Carrier Upgrades",
                                      [new MapLocation("levels", 1722, 1160, LevelsIconSize, BorderThickness)],
                                      ecUpgrades);
        var iceCap = new Location("Ice Cap Upgrade",
                                  [new MapLocation("levels", 1936, 192, LevelsIconSize, BorderThickness)],
                                  icUpgrades);
        var upgrades = new[] { stationSquare, mysticRuins, eggCarrier, iceCap };
        await FileWriter.WriteFile(JsonSerializer.Serialize(upgrades, Constants.JsonOptions),
                                   "upgrades.json",
                                   "locations");

        IEnumerable<string>? GetAccessRules(string section) => logic.First(entry =>
        {
            var upgrade = entry.Upgrade.Split('.', StringSplitOptions.TrimEntries)[^1].Humanize(LetterCasing.Title);
            return section.StartsWith(upgrade);
        }).BuildAccessRules();
    }
}