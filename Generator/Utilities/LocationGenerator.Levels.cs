using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int SonicLevelsStart = 543801000;
    private const int SonicLevelsEnd = 543802000;
    private const int TailsLevelsStart = 543802000;
    private const int TailsLevelsEnd = 543803000;
    private const int KnucklesLevelsStart = 543803000;
    private const int KnucklesLevelsEnd = 543804000;
    private const int AmyLevelsStart = 543804000;
    private const int AmyLevelsEnd = 543805000;
    private const int GammaLevelsStart = 543805000;
    private const int GammaLevelsEnd = 543806000;
    private const int BigLevelsStart = 543806000;
    private const int BigLevelsEnd = 543807000;

    private static async Task GenerateLevels(FrozenDictionary<int, string> dict)
    {
        static IEnumerable<string>? GetMissionVisibility(string character, string section) => section[^1] switch
        {
            'S' => [$"{character}MissionS"],
            'A' => [$"{character}MissionS", $"{character}MissionA"],
            'B' => [$"{character}MissionS", $"{character}MissionA", $"{character}MissionB"],
            'C' => [$"{character}MissionS", $"{character}MissionA", $"{character}MissionB", $"{character}MissionC"],
            _ => default
        };
        static IEnumerable<Location> GetLevels(FrozenDictionary<int, string> dict, int start, int end, int x, int y0)
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
                   let y = y0 + 128 * level.Multipler
                   let character = CharacterParser().Match(level.Location.Key).Groups[1].Value
                   select new Location(level.Location.Key,
                                       [new MapLocation("levels", x, y, LevelsIconSize, BorderThickness)],
                                       from section in level.Location
                                       select new Section(section,
                                                          VisibilityRules: GetMissionVisibility(character, section)),
                                       VisibilityRules: [$"{character}Playable"]);
        }

        var sonicLevels = GetLevels(dict, SonicLevelsStart, SonicLevelsEnd, 58, 64);
        var tailsLevels = GetLevels(dict, TailsLevelsStart, TailsLevelsEnd, 570, 64);
        var knucklesLevels = GetLevels(dict, KnucklesLevelsStart, KnucklesLevelsEnd, 570, 704);
        var amyLevels = GetLevels(dict, AmyLevelsStart, AmyLevelsEnd, 1082, 64);
        var gammaLevels = GetLevels(dict, GammaLevelsStart, GammaLevelsEnd, 1082, 448);
        var bigLevels = GetLevels(dict, BigLevelsStart, BigLevelsEnd, 1594, 64);
        var levels = sonicLevels.Union(tailsLevels)
                                .Union(knucklesLevels)
                                .Union(amyLevels)
                                .Union(gammaLevels)
                                .Union(bigLevels);
        await FileWriter.WriteFile(JsonSerializer.Serialize(levels, Constants.JsonOptions),
                                   "levels.json",
                                   "locations");
    }
}