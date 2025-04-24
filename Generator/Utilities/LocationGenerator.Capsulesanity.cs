using System.Collections.Frozen;
using System.Text.Json;
using Humanizer;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int SonicCapsulesStart = 543810000;
    private const int SonicCapsulesEnd = 543820000;
    private const int TailsCapsulesStart = 543820000;
    private const int TailsCapsulesEnd = 543830000;
    private const int KnucklesCapsulesStart = 543830000;
    private const int KnucklesCapsulesEnd = 543840000;
    private const int AmyCapsulesStart = 543840000;
    private const int AmyCapsulesEnd = 543850000;
    private const int GammaCapsulesStart = 543850000;
    private const int GammaCapsulesEnd = 543860000;
    private const int BigCapsulesStart = 543860000;
    private const int BigCapsulesEnd = 543870000;
    private const int PinballCapsulesStart = 543812548;
    private const int PinballCapsulesEnd = 543812553;

    private static IEnumerable<LuaLocation> GenerateCapsulesLua(FrozenDictionary<int, string> dict)
        => from entry in dict
           where entry.Key >= SonicCapsulesStart && entry.Key < BigCapsulesEnd && entry.Key % 1000 >= 500
           let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
           let name = parts[0]
           let section = parts[1]
           select new LuaLocation(entry.Key, $"Capsulesanity - {name}", section);

    private static async Task GenerateCapsules(FrozenDictionary<int, string> dict)
    {
        static string? GetVisibility(string name) => name switch
        {
            "Extra Life" => "LifeCapsulesanity",
            "Shield" => "ShieldCapsulesanity",
            "Magnetic Shield" => "ShieldCapsulesanity",
            "Speed Up" => "PowerupCapsulesanity",
            "Invincibility" => "PowerupCapsulesanity",
            "Bomb" => "PowerupCapsulesanity",
            "Five Rings" => "RingCapsulesanity",
            "Ten Rings" => "RingCapsulesanity",
            "Random Rings" => "RingCapsulesanity",
            _ => default
        };

        var logic = await LogicLoader.LoadForCapsule().ToListAsync();
        var sonicCapsules = GetCapsules(dict, SonicCapsulesStart, SonicCapsulesEnd, 58, 64);
        var tailsCapsules = GetCapsules(dict, TailsCapsulesStart, TailsCapsulesEnd, 570, 64);
        var knucklesCapsules = GetCapsules(dict, KnucklesCapsulesStart, KnucklesCapsulesEnd, 570, 704);
        var amyCapsules = GetCapsules(dict, AmyCapsulesStart, AmyCapsulesEnd, 1082, 64);
        var gammaCapsules = GetCapsules(dict, GammaCapsulesStart, GammaCapsulesEnd, 1082, 448);
        var bigCapsules = GetCapsules(dict, BigCapsulesStart, BigCapsulesEnd, 1594, 64);
        var capsules = sonicCapsules.Union(tailsCapsules)
                                    .Union(knucklesCapsules)
                                    .Union(amyCapsules)
                                    .Union(gammaCapsules)
                                    .Union(bigCapsules);
        await FileWriter.WriteFile(JsonSerializer.Serialize(capsules, Constants.JsonOptions),
                                   "capsules.json",
                                   "locations");

        IEnumerable<Location> GetCapsules(FrozenDictionary<int, string> dict, int start, int end, int x, int y0)
        {
            var locations = from entry in dict
                            where entry.Key >= start && entry.Key < end && entry.Key % 1000 >= 500
                            orderby entry.Key
                            let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
                            let name = parts[0]
                            let section = parts[1]
                            group (entry.Key, Name: section) by name;
            var multipliers = locations.First().Key switch
            {
                string s when s.Contains("(T") => [0, 1, 3, 4],
                _ => Enumerable.Range(0, locations.Count())
            };
            var capsules = locations.Zip(multipliers, (l, m) => (Location: l, Multipler: m));
            return from level in capsules
                   let y = y0 + 128 * level.Multipler
                   let character = CharacterParser().Match(level.Location.Key).Groups[1].Value
                   let index = level.Location.Key.IndexOf("(")
                   let access = level.Location.Key[0..(index - 1)]
                   select new Location($"Capsulesanity - {level.Location.Key}",
                                       [new MapLocation("capsules", x, y, LevelsIconSize, BorderThickness)],
                                       from section in level.Location
                                       let item = CapsuleItemParser().Match(section.Name).Groups[1].Value
                                       let visibility = GetVisibility(item)
                                       let pinball = section.Key >= PinballCapsulesStart && section.Key < PinballCapsulesEnd
                                       select new Section(section.Name,
                                                          AccessRules: GetAccessRules(level.Location.Key,
                                                                                      character,
                                                                                      section.Name),
                                                          VisibilityRules: pinball
                                                          ? [$"PinballCapsules,{visibility}"]
                                                          : [visibility]),
                                       AccessRules: [$"$CanAccess|{character}|{access},Playable{character}"],
                                       VisibilityRules: [$"{character}Playable,Capsulesanity,{character}Capsulesanity"]);
        }

        IEnumerable<string>? GetAccessRules(string location, string character, string section) => logic.First(entry =>
        {
            var level = entry.Level.Humanize(LetterCasing.Title);
            var type = entry.Type.Humanize(LetterCasing.Title);
            var capsule = SanityTypeParser().Match(section).Groups[1].Value;
            var number = int.Parse(NumberParser().Match(section).Value);
            return location.StartsWith(level)
                   && character.Equals(entry.Character)
                   && capsule.Equals(type, StringComparison.InvariantCultureIgnoreCase)
                   && number == entry.Number;
        }).BuildAccessRules();
    }
}