using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int SonicEnemiesStart = 543810000;
    private const int SonicEnemiesEnd = 543820000;
    private const int TailsEnemiesStart = 543820000;
    private const int TailsEnemiesEnd = 543830000;
    private const int KnucklesEnemiesStart = 543830000;
    private const int KnucklesEnemiesEnd = 543840000;
    private const int AmyEnemiesStart = 543840000;
    private const int AmyEnemiesEnd = 543850000;
    private const int GammaEnemiesStart = 543850000;
    private const int GammaEnemiesEnd = 543860000;
    private const int BigEnemiesStart = 543860000;
    private const int BigEnemiesEnd = 543870000;

    private const string EnemiesMap = "enemies";

    private static int[] MissableEnemies =
    [
        .. Enumerable.Range(543812001, 10),
        .. Enumerable.Range(543814001, 8),
        .. Enumerable.Range(543821001, 3)
    ];

    private static IEnumerable<LuaLocation> GenerateEnemiesLua(FrozenDictionary<int, string> dict)
        => from entry in dict
           where entry.Key >= SonicEnemiesStart && entry.Key < BigEnemiesEnd && entry.Key % SanitiesModulus < SanitiesMidpoint
           let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
           let name = parts[0]
           let section = parts[1]
           select new LuaLocation(entry.Key, $"Enemysanity - {name}", section);

    private static async Task GenerateEnemies(FrozenDictionary<int, string> dict)
    {
        var sonicEnemies = GetEnemies(dict, SonicEnemiesStart, SonicEnemiesEnd, SonicLevelsX, SonicLevelsY);
        var tailsEnemies = GetEnemies(dict, TailsEnemiesStart, TailsEnemiesEnd, TailsLevelsX, TailsLevelsY);
        var knucklesEnemies = GetEnemies(dict, KnucklesEnemiesStart, KnucklesEnemiesEnd, KnucklesLevelsX, KnucklesLevelsY);
        var amyEnemies = GetEnemies(dict, AmyEnemiesStart, AmyEnemiesEnd, AmyLevelsX, AmyLevelsY);
        var gammaEnemies = GetEnemies(dict, GammaEnemiesStart, GammaEnemiesEnd, GammaLevelsX, GammaLevelsY);
        var bigEnemies = GetEnemies(dict, BigEnemiesStart, BigEnemiesEnd, BigLevelsX, BigLevelsY);
        var Enemies = sonicEnemies.Union(tailsEnemies)
                                .Union(knucklesEnemies)
                                .Union(amyEnemies)
                                .Union(gammaEnemies)
                                .Union(bigEnemies);
        await FileWriter.WriteFile(JsonSerializer.Serialize(Enemies, Constants.JsonOptions),
                                   "enemies.json",
                                   "locations");

        IEnumerable<Location> GetEnemies(FrozenDictionary<int, string> dict, int start, int end, int x, int y0)
        {
            var locations = from entry in dict
                            where entry.Key >= start && entry.Key < end && entry.Key % SanitiesModulus < SanitiesMidpoint
                            orderby entry.Key
                            let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
                            let name = parts[0]
                            let section = parts[1]
                            group new { entry.Key, Name = section } by name;
            var multipliers = locations.First().Key switch
            {
                string s when s.Contains("(T") => [0, 1, 3, 4],
                string s when s.Contains("(B") => [0, 2, 3],
                _ => Enumerable.Range(0, locations.Count())
            };
            var enemies = locations.Zip(multipliers, (l, m) => (Location: l, Multipler: m));
            return from level in enemies
                   let y = y0 + LevelsSpacingY * level.Multipler
                   let character = CharacterParser().Match(level.Location.Key).Groups[1].Value
                   select new Location($"Enemysanity - {level.Location.Key}",
                                       [new MapLocation(EnemiesMap, x, y, LevelsIconSize, BorderThickness)],
                                       from section in level.Location
                                       let missable = MissableEnemies.Contains(section.Key)
                                       select new Section(section.Name,
                                                          VisibilityRules: missable ? ["MissableEnemies"] : default),
                                       VisibilityRules: [$"{character}Playable,Enemysanity,{character}Enemysanity"]);
        }
    }
}