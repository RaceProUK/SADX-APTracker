using System.Collections.Frozen;
using System.Text.Json;
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
                   select new Location($"Fishsanity - {level.Location.Key}",
                                       [new MapLocation("levels", 1594 + 48, y, LevelsIconSize, BorderThickness)],
                                       from section in level.Location select new Section(section),
                                       VisibilityRules: [$"BigPlayable,Fishsanity"]);
        await FileWriter.WriteFile(JsonSerializer.Serialize(fish, Constants.JsonOptions),
                                   "fish.json",
                                   "locations");
    }
}