using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LocationGenerator
{
    private const int MissionsStart = 543800800;
    private const int MissionsEnd = 543800900;

    private static IEnumerable<LuaLocation> GenerateMissionsLua(FrozenDictionary<int, string> dict)
        => from entry in dict
           where entry.Key >= MissionsStart && entry.Key < MissionsEnd
           let number = int.Parse(NumberParser().Match(entry.Value).Value)
           select new LuaLocation(entry.Key, entry.Value, Constants.MissionBriefs[number]);

    private static async Task GenerateMissions(FrozenDictionary<int, string> dict)
    {
        static int CalculateX(int number) => 56 * ((number - 1) % 10) + 38;
        static int CalculateY(int number) => 44 * ((number - 1) / 10) + 30;
        static string GetMissionCharacter(int number) => number switch
        {
            4 or 10 or 16 or 24 or 31 or 37 or 47 or 54 => "Tails",
            5 or 12 or 25 or 26 or 32 or 38 or 48 or 56 or 59 => "Knuckles",
            6 or 18 or 19 or 43 or 50 => "Amy",
            7 or 21 or 39 or 42 or 51 => "Gamma",
            8 or 14 or 22 or 29 or 35 or 44 or 52 or 60 => "Big",
            _ => "Sonic"
        };

        var logic = await LogicLoader.LoadForMission().ToListAsync();
        var missions = from entry in dict
                       where entry.Key >= MissionsStart && entry.Key < MissionsEnd
                       let number = int.Parse(NumberParser().Match(entry.Value).Value)
                       let x = CalculateX(number)
                       let y = CalculateY(number)
                       let character = GetMissionCharacter(number)
                       select new Location(entry.Value,
                                           [new MapLocation("missions", x, y, MissionsIconSize, BorderThickness)],
                                           [new Section(Constants.MissionBriefs[number])],
                                           AccessRules: GetAccessRules(number, character),
                                           VisibilityRules: [$"MissionsRequired,{character}Playable,AllowMission{number}"]);
        await FileWriter.WriteFile(JsonSerializer.Serialize(missions, Constants.JsonOptions),
                                   "missions.json",
                                   "locations");

        IEnumerable<string> GetAccessRules(int number, string character)
        {
            var rules = logic.First(entry => number == entry.Number)
                             .BuildAccessRules()?
                             .Select(_ => $"Playable{character},{_}");
            return rules ?? [$"Playable{character}"];
        }
    }
}