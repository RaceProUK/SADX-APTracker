using System.Data;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class AccessRulesGenerator
{
    internal static IEnumerable<string> Levels { get; private set; } = [];

    internal static IEnumerable<string> Characters { get; private set; } = [];

    internal static async Task Generate()
    {
        await GenerateLevelAccessRules();
    }

    private static async Task GenerateLevelAccessRules()
    {
        var logic = await LogicLoader.LoadForAreaToLevel().ToListAsync();
        Levels = [.. logic.Select(_ => _.Level).Distinct()];
        Characters = [.. logic.Select(_ => _.Character).Distinct()];

        var entries = from character in Characters
                      from level in Levels
                      from logicLevel in Enumerable.Range(0, 4)
                      let rule = MakeLogicRule(character, level, logicLevel)
                      select $"    [\"{character} - {level} - {logicLevel}\"] = function() return {rule} end,";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["AccessRules = {", .. entries, "}"]),
                                               "accessRules.lua",
                                               "scripts",
                                               "logic");

        string MakeLogicRule(string character, string level, int logicLevel)
        {
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