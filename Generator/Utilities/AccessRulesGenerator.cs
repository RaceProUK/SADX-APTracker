using System.Data;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class AccessRulesGenerator
{
    private static readonly IEnumerable<string> Levels =
    [
        "Emerald Coast",
        "Windy Valley",
        "Casinopolis",
        "Ice Cap",
        "Twinkle Park",
        "Speed Highway",
        "Red Mountain",
        "Sky Deck",
        "Lost World",
        "Final Egg",
        "Hot Shelter"
    ];

    private static readonly IEnumerable<string> Characters =
    [
        "Sonic",
        "Tails",
        "Knuckles",
        "Amy",
        "Gamma",
        "Big"
    ];

    private static readonly IEnumerable<int> LogicLevels = Enumerable.Range(0, 4);

    internal static async Task Generate()
    {
        var logic = await LogicLoader.LoadForAreaToLevel().ToListAsync();
        var entries = from character in Characters
                      from level in Levels
                      from logicLevel in LogicLevels
                      let rule = MakeLogicRule(character, level, logicLevel)
                      select $"    [\"{character} - {level} - {logicLevel}\"] = function() return {rule} end,";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["AccessRules = {", .. entries, "}"]),
                                               "accessRules.lua",
                                               "scripts");

        string MakeLogicRule(string character, string level, int logicLevel)
        {
            level = Common.RemoveWhitespace(level);

            var spec = logic.First(_ => character.Equals(_.Character) && level.Equals(_.Level));
            var set = logicLevel switch
            {
                0 => spec.NormalLogic,
                1 => spec.HardLogic,
                2 => spec.ExpertDCLogic,
                3 => spec.ExpertDXLogic,
                _ => []
            };
            var rules = set.Select(_ => string.Join(" and ", _.Select(_ => $"HasItem(\"{_}\")")));
            return rules.Any() ? string.Join(" or ", rules) : "true";
        }
    }
}