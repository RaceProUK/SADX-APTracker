using System.Data;
using QuikGraph;
using QuikGraph.Algorithms;
using RPS.SADX.PopTracker.Generator.Models.Logic;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class AccessRulesGenerator
{
    internal static IEnumerable<string> Areas { get; private set; } = [];

    internal static IEnumerable<string> Levels { get; private set; } = [];

    internal static IEnumerable<string> Characters { get; private set; } = [];

    internal static async Task Generate()
    {
        await GenerateAreaReachRules();
        await GenerateLevelAccessRules();
    }

    private static async Task GenerateAreaReachRules()
    {
        var logic = await LogicLoader.LoadForAreaToArea().ToListAsync();
        var graphs = logic.GroupBy(_ => _.Character)
                          .ToDictionary(_ => _.Key,
                                        _ => _.ToBidirectionalGraph<string, AreaToArea>());
        Areas = [.. logic.Select(_ => _.AreaFrom).Distinct()];
        Characters = [.. logic.Select(_ => _.Character).Distinct()];

        var entries = from character in Characters
                      from areaFrom in Areas
                      from areaTo in Areas
                      where !string.Equals(areaFrom, areaTo, StringComparison.OrdinalIgnoreCase)
                      from logicLevel in Enumerable.Range(0, 5)
                      let rule = MakeLogicRule(character, areaFrom, areaTo, logicLevel)
                      select $"    [\"{character} - {areaFrom} - {areaTo} - {logicLevel}\"] = function() return {rule} end,";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["ReachRules = {", .. entries, "}"]),
                                               "reachRules.lua",
                                               "scripts",
                                               "logic");

        string MakeLogicRule(string character, string from, string to, int logicLevel)
        {
            if (!graphs.TryGetValue(character, out var graph))
                return "false";

            var itemSets = new List<HashSet<string>>();
            foreach (var path in graph.RankedShortestPathHoffmanPavley(_ => 1, @from, to, 3).OrderBy(_ => _.Count()))
            {
                var steps = path.Select(_ => logicLevel switch
                {
                    0 => _.NormalLogic,
                    1 => _.HardLogic,
                    2 => _.ExpertDCLogic,
                    3 => _.ExpertDXLogic,
                    4 => _.ExpertDXPlusLogic,
                    _ => []
                });
                if (steps.All(_ => !_.Any()))
                    break; // If no item is needed for the shortest path, then there are no item sets
                else
                {
                    // Build an item set for each chain
                    foreach (var chain in CartesianProduct(steps))
                    {
                        var items = chain.SelectMany(_ => _);
                        if (items.Any()) //But only if said chain has items
                            itemSets.Add([.. items]);
                    }
                }
            }
            var rules = itemSets.Distinct(HashSet<string>.CreateSetComparer())
                                .Select(_ => string.Join(" and ", _.Select(_ => $"HasItem(\"{_}\")")));
            return rules.Any() ? string.Join(" or ", rules.OrderBy(_ => _.Length)) : "true";
        }
    }

    private static async Task GenerateLevelAccessRules()
    {
        var logic = await LogicLoader.LoadForAreaToLevel().ToListAsync();
        Levels = [.. logic.Select(_ => _.Level).Distinct()];
        Characters = [.. logic.Select(_ => _.Character).Distinct()];

        var entries = from character in Characters
                      from level in Levels
                      from logicLevel in Enumerable.Range(0, 5)
                      let rule = MakeLogicRule(character, level, logicLevel)
                      select $"    [\"{character} - {level} - {logicLevel}\"] = function() return {rule} end,";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["AccessRules = {", .. entries, "}"]),
                                               "accessRules.lua",
                                               "scripts",
                                               "logic");

        //Also write a level-entrance lookup because of Entrance Randomiser
        var mappings = logic.Select(_ => $"    [\"{_.Character} - {_.Level}\"] = \"{_.Area}\",");
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["EntranceAreas = {", .. mappings, "}"]),
                                               "entranceAreas.lua",
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
                4 => spec.ExpertDXPlusLogic,
                _ => []
            };
            var rules = set.Select(_ => string.Join(" and ", _.Select(_ => $"HasItem(\"{_}\")")));
            return rules.Any() ? string.Join(" or ", rules) : "true";
        }
    }

    private static IEnumerable<IEnumerable<LogicRule>> CartesianProduct(IEnumerable<LogicRules> sequences)
    {
        IEnumerable<IEnumerable<LogicRule>> emptyProduct = [[]];
        return sequences.Where(_ => _.Any())
                        .Aggregate(emptyProduct, (accumulator, sequence) => from accseq in accumulator
                                                                            from item in sequence
                                                                            select accseq.Concat([item]));
    }
}