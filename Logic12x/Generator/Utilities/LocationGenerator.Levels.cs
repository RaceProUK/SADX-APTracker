using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int SonicLevels = 543801000;
    private const int SonicLevelsEnd = 543802000;
    private const int TailsLevels = 543802000;
    private const int TailsLevelsEnd = 543803000;
    private const int KnucklesLevels = 543803000;
    private const int KnucklesLevelsEnd = 543804000;
    private const int AmyLevels = 543804000;
    private const int AmyLevelsEnd = 543805000;
    private const int GammaLevels = 543805000;
    private const int GammaLevelsEnd = 543806000;
    private const int BigLevels = 543806000;
    private const int BigLevelsEnd = 543807000;

    private static IEnumerable<LuaLocation> GenerateLevelsLua(FrozenDictionary<int, string> dict)
        => from entry in dict
           where entry.Key >= SonicLevels && entry.Key < BigLevelsEnd
           let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
           let name = parts[0]
           let section = parts[1]
           select new LuaLocation(entry.Key, name, section);

    private static async Task GenerateLevels(FrozenDictionary<int, string> dict)
    {
        var sonicLevels = GetLevels(dict, SonicLevels, SonicLevelsEnd, SonicLevelsX, SonicLevelsY);
        var tailsLevels = GetLevels(dict, TailsLevels, TailsLevelsEnd, TailsLevelsX, TailsLevelsY);
        var knucklesLevels = GetLevels(dict, KnucklesLevels, KnucklesLevelsEnd, KnucklesLevelsX, KnucklesLevelsY);
        var amyLevels = GetLevels(dict, AmyLevels, AmyLevelsEnd, AmyLevelsX, AmyLevelsY);
        var gammaLevels = GetLevels(dict, GammaLevels, GammaLevelsEnd, GammaLevelsX, GammaLevelsY);
        var bigLevels = GetLevels(dict, BigLevels, BigLevelsEnd, BigLevelsX, BigLevelsY);
        var levels = sonicLevels.Union(tailsLevels)
                                .Union(knucklesLevels)
                                .Union(amyLevels)
                                .Union(gammaLevels)
                                .Union(bigLevels);
        await FileWriter.WriteFile(JsonSerializer.Serialize(levels, Constants.JsonOptions),
                                   "levels.json",
                                   "locations");

        IEnumerable<Location> GetLevels(FrozenDictionary<int, string> dict, int start, int end, int x, int y0)
        {
            var locations = from entry in dict
                            where entry.Key >= start && entry.Key < end
                            let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
                            let name = parts[0]
                            let section = parts[1]
                            group section by name;
            var multipliers = Enumerable.Range(0, locations.Count());
            var levels = locations.Zip(multipliers, (l, m) => (Location: l, Multipler: m));
            return from level in levels
                   let y = y0 + LevelsSpacingY * level.Multipler
                   select new Location(level.Location.Key,
                                       [new MapLocation(LevelsMap, x, y, LevelsIconSize, BorderThickness)],
                                       from section in level.Location
                                       orderby section[^1] ^ 0b0001_0000
                                       select new Section(section));
        }
    }
}