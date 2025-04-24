using System.Collections.Frozen;
using System.Text.Json;
using Humanizer;
using RPS.SADX.PopTracker.Generator.Models;
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

    private static IEnumerable<LuaLocation> GenerateLevelsLua(FrozenDictionary<int, string> dict)
        => from entry in dict
           where entry.Key >= SonicLevelsStart && entry.Key < BigLevelsEnd
           let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
           let name = parts[0]
           let section = parts[1]
           select new LuaLocation(entry.Key, name, section);

    private static async Task GenerateLevels(FrozenDictionary<int, string> dict)
    {
        static IEnumerable<string>? GetMissionVisibility(string character, string section) => [$"{character}Mission{section[^1]}"];

        var missionLogic = await LogicLoader.LoadForLevelMission().ToListAsync();
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
                   let y = y0 + 128 * level.Multipler
                   let character = CharacterParser().Match(level.Location.Key).Groups[1].Value
                   let index = level.Location.Key.IndexOf("(")
                   let access = level.Location.Key[0..(index - 1)]
                   select new Location(level.Location.Key,
                                       [new MapLocation("levels", x, y, LevelsIconSize, BorderThickness)],
                                       from section in level.Location
                                       select new Section(section,
                                                          AccessRules: GetMissionAccessRules(level.Location.Key, character, section),
                                                          VisibilityRules: GetMissionVisibility(character, section)),
                                       AccessRules: [$"$CanAccess|{character}|{access},Playable{character}"],
                                       VisibilityRules: [$"{character}Playable"]);
        }

        IEnumerable<string>? GetMissionAccessRules(string location, string character, string mission)
            => missionLogic.First(entry =>
            {
                var level = entry.Level.Humanize(LetterCasing.Title);
                return location.StartsWith(level) && character.Equals(entry.Character) && mission.EndsWith(entry.Mission);
            }).BuildAccessRules();
    }
}