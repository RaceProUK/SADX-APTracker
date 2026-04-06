using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const string LevelsMap = "levels";

    private const int LevelsIconSize = 32;
    private const int MissionsIconSize = 16;
    private const int BorderThickness = 1;

    private const int LevelsSpacingY = 128;
    private const int SubGamesSpacingY = 64;
    private const int FieldSpacingY = 260;

    private const int SonicLevelsX = 58;
    private const int SonicLevelsY = 64;

    private const int TailsLevelsX = 570;
    private const int TailsLevelsY = 64;

    private const int KnucklesLevelsX = 570;
    private const int KnucklesLevelsY = 704;

    private const int AmyLevelsX = 1082;
    private const int AmyLevelsY = 64;

    private const int GammaLevelsX = 1082;
    private const int GammaLevelsY = 448;

    private const int BigLevelsX = 1594;
    private const int BigLevelsY = 64;

    private const int SubGamesLevelsX = 1082;
    private const int SubGamesLevelsY = 1056;

    private const int FieldItemsY = 640;

    private const int SanitiesModulus = 1000;
    private const int SanitiesMidpoint = SanitiesModulus / 2;

    [GeneratedRegex("\\(([A-Za-z ]+)\\)")]
    private static partial Regex CapsuleItemParser();

    [GeneratedRegex("\\(([A-Za-z]+)\\)")]
    private static partial Regex CharacterParser();

    [GeneratedRegex("\\(([A-Za-z ]+)\\)")]
    private static partial Regex SanityTypeParser();

    [GeneratedRegex("(\\d+)")]
    private static partial Regex NumberParser();

    [GeneratedRegex("Sub-Level - ([A-Za-z ]+)")]
    private static partial Regex SublevelParser();

    internal static async Task Generate(IDictionary<string, int> dict)
    {
        var idToName = dict.ToFrozenDictionary(_ => _.Value, _ => _.Key);
        await GenerateLocationMapLua(idToName);
        await GenerateLevels(idToName);
        await GenerateSubGames(idToName);
        await GenerateUpgrades(idToName);
        await GenerateBosses(idToName);
        await GenerateFieldEmblems(idToName);
        await GenerateChaoEggs(idToName);
        await GenerateChaoRaces(idToName);
        await GenerateEnemies(idToName);
        await GenerateCapsules(idToName);
        await GenerateFish(idToName);
        await GenerateMissions(idToName);
    }

    private static async Task GenerateLocationMapLua(FrozenDictionary<int, string> dict)
    {
        var levels = GenerateLevelsLua(dict);
        var subGames = GenerateSubGamesLua(dict);
        var upgrades = GenerateUpgradesLua(dict);
        var bosses = GenerateBossesLua(dict);
        var fieldEmblems = GenerateFieldEmblemsLua(dict);
        var chaoEggs = GenerateChaoEggsLua(dict);
        var chaoRaces = GenerateChaoRacesLua(dict);
        var enemies = GenerateEnemiesLua(dict);
        var capsules = GenerateCapsulesLua(dict);
        var fish = GenerateFishLua(dict);
        var missions = GenerateMissionsLua(dict);
        var locations = levels.Union(subGames)
                              .Union(upgrades)
                              .Union(bosses)
                              .Union(fieldEmblems)
                              .Union(chaoEggs)
                              .Union(chaoRaces)
                              .Union(enemies)
                              .Union(capsules)
                              .Union(fish)
                              .Union(missions);
        var entries = from location in locations
                      orderby location.Id
                      select $"    [{location.Id}] = {{ \"{location.Area}\", \"{location.Section}\" }},";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["LocationMap = {", .. entries, "}"]),
                                   "locationMap.lua",
                                   "scripts",
                                   "archipelago");
    }
}