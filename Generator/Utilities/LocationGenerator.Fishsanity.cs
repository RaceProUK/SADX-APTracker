using System;
using System.Collections.Frozen;
using System.Text.Json;
using Humanizer;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int FishStart = 543800950;
    private const int FishEnd = 543801000;

    private static IEnumerable<LuaLocation> GenerateFishLua(FrozenDictionary<int, string> dict)
        => from entry in dict
           where entry.Key >= FishStart && entry.Key < FishEnd
           let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
           let name = parts[0]
           let section = parts[1]
           select new LuaLocation(entry.Key, $"Fishsanity - {name}", section);

    private static async Task GenerateFish(FrozenDictionary<int, string> dict)
    {
        static int GetOrder(string name) => name[0] switch
        {
            'T' => 0,
            'I' => 1,
            'E' => 2,
            'H' => 3,
            _ => int.MaxValue
        };

        var logic = await LogicLoader.LoadForFish().ToListAsync();
        var locations = from entry in dict
                        where entry.Key >= FishStart && entry.Key < FishEnd
                        orderby GetOrder(entry.Value), entry.Key
                        let parts = entry.Value.Split('-', StringSplitOptions.TrimEntries)
                        let name = parts[0]
                        let section = parts[1]
                        group section by name;
        var multipliers = Enumerable.Range(0, locations.Count());
        var fish = from level in locations.Zip(multipliers, (l, m) => (Location: l, Multipler: m))
                   let y = 64 + 128 * level.Multipler
                   let index = level.Location.Key.IndexOf('(')
                   let access = Common.RemoveWhitespace(level.Location.Key[0..(index - 1)])
                   select new Location($"Fishsanity - {level.Location.Key}",
                                       [new MapLocation("levels", 1594 + 48, y, LevelsIconSize, BorderThickness)],
                                       from section in level.Location
                                       select new Section(section,
                                                          AccessRules: GetAccessRules(level.Location.Key, section)),
                                       AccessRules: [$"$CanAccess|Big|{access},PlayableBig"],
                                       VisibilityRules: [$"BigPlayable,Fishsanity"]);
        await FileWriter.WriteFile(JsonSerializer.Serialize(fish, Constants.JsonOptions),
                                   "fish.json",
                                   "locations");

        IEnumerable<string>? GetAccessRules(string location, string section) => logic.First(entry =>
        {
            var level = entry.Level.Humanize(LetterCasing.Title);
            var fish = entry.Type.Humanize(LetterCasing.Title);
            return location.StartsWith(level) && section.Equals(fish);
        }).BuildAccessRules();
    }
}