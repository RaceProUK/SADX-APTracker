using System.Data;
using Humanizer;
using QuikGraph;
using QuikGraph.Algorithms;
using RPS.SADX.PopTracker.Generator.Models.Logic;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class AccessRulesGenerator
{
    internal static IEnumerable<string> Characters { get; private set; } = [];

    internal static async Task Generate()
    {
        var logic = await LogicLoader.LoadForConnections().ToListAsync();
        Characters = [.. logic.Select(_ => _.Character).Distinct()];
        await GenerateAccessRules(logic);
        await GenerateRoutes(logic);
    }

    private static async Task GenerateAccessRules(List<Connection> logic)
    {
        var entries = from ruleSet in logic
                      from logicLevel in Enumerable.Range(0, 5)
                      let rule = MakeLogicRule(ruleSet.Character, ruleSet.AreaFrom, ruleSet.AreaTo, logicLevel)
                      where !string.IsNullOrWhiteSpace(rule)
                      select $"    [\"{ruleSet.Character} - {ruleSet.AreaFrom} - {ruleSet.AreaTo} - {logicLevel}\"] = function() return {rule} end,";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["AccessRules = {", .. entries, "}"]),
                                               "accessRules.lua",
                                               "scripts",
                                               "logic");

        string MakeLogicRule(string character, string areaFrom, string areaTo, int logicLevel)
        {
            var spec = logic.First(_ => character.Equals(_.Character) && areaFrom.Equals(_.AreaFrom) && areaTo.Equals(_.AreaTo));
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
            return string.Join(" or ", rules);
        }
    }

    private static async Task GenerateRoutes(List<Connection> logic)
    {
        var graph = logic.GroupBy(_ => _.Character)
                         .ToDictionary(_ => _.Key,
                                       _ => _.ToBidirectionalGraph<string, Connection>())
                         .First().Value; //All graphs will be identical so only use one
        var entries = from areaFrom in logic.Select(_ => _.AreaFrom).Distinct()
                      from areaTo in logic.Select(_ => _.AreaTo).Distinct()
                      where !string.Equals(areaFrom, areaTo, StringComparison.OrdinalIgnoreCase)
                      let routes = MakeRoutes(areaFrom, areaTo)
                      select $"    [\"{areaFrom} - {areaTo}\"] = [[{string.Join("], [", routes)}]],";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["Routes = {", .. entries, "}"]),
                                               "routes.lua",
                                               "scripts",
                                               "logic");
        
        IEnumerable<string> MakeRoutes(string areaFrom, string areaTo)
        {
            foreach (var path in graph.RankedShortestPathHoffmanPavley(_ => 1, areaFrom, areaTo, 81).OrderBy(_ => _.Count()))
            {
                yield return string.Join(", ", path.Select(_ => $"\"{_.AreaFrom} - {_.AreaTo}\""));
            }
        }
    }
}