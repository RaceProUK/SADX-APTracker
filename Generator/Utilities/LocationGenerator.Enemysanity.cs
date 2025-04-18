using System.Collections.Frozen;
using System.Text.Json;
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

    private static async Task GenerateEnemies(FrozenDictionary<int, string> dict)
    {
        static IEnumerable<Location> GetEnemies(FrozenDictionary<int, string> dict, int start, int end, int x, int y0)
        {
            var locations = from entry in dict
                            where entry.Key >= start && entry.Key < end && entry.Key % 1000 < 500
                            orderby entry.Key
                            let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
                            let name = parts[0]
                            let section = parts[1]
                            group section by name;
            var multipliers = locations.First().Key switch
            {
                string s when s.Contains("(T") => [0, 1, 3, 4],
                string s when s.Contains("(B") => [0, 2, 3],
                _ => Enumerable.Range(0, locations.Count())
            };
            var enemies = locations.Zip(multipliers, (l, m) => (Location: l, Multipler: m));
            return from level in enemies
                   let y = y0 + 128 * level.Multipler
                   select new Location($"Enemysanity - {level.Location.Key}",
                                       [new MapLocation("enemies", x, y, LevelsIconSize, BorderThickness)],
                                       from section in level.Location select new Section(section));
        }

        var sonicEnemies = GetEnemies(dict, SonicEnemiesStart, SonicEnemiesEnd, 58, 64);
        var tailsEnemies = GetEnemies(dict, TailsEnemiesStart, TailsEnemiesEnd, 570, 64);
        var knucklesEnemies = GetEnemies(dict, KnucklesEnemiesStart, KnucklesEnemiesEnd, 570, 704);
        var amyEnemies = GetEnemies(dict, AmyEnemiesStart, AmyEnemiesEnd, 1082, 64);
        var gammaEnemies = GetEnemies(dict, GammaEnemiesStart, GammaEnemiesEnd, 1082, 448);
        var bigEnemies = GetEnemies(dict, BigEnemiesStart, BigEnemiesEnd, 1594, 64);
        var Enemies = sonicEnemies.Union(tailsEnemies)
                                .Union(knucklesEnemies)
                                .Union(amyEnemies)
                                .Union(gammaEnemies)
                                .Union(bigEnemies);
        await FileWriter.WriteFile(JsonSerializer.Serialize(Enemies, Constants.JsonOptions),
                                   "enemies.json",
                                   "locations");
    }
}