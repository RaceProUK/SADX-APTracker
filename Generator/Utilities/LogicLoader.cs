using Microsoft.Extensions.Configuration;
using RPS.SADX.PopTracker.Generator.Models.Logic;
using SheetToObjects.Adapters.GoogleSheets;
using SheetToObjects.Lib;
using SheetToObjects.Lib.FluentConfiguration;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class LogicLoader
{
    private static string ApiKey { get; }

    static LogicLoader()
    {
        var config = new ConfigurationBuilder().AddUserSecrets("b142e81d-996d-4b19-a68a-38a41c45604e").Build();
        ApiKey = config["GoogleApiKey"]!;
    }

    internal static IAsyncEnumerable<AreaToArea> LoadForAreaToArea()
        => LoadFor<AreaToArea>("B72:I216", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.AreaFrom))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.AreaTo))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<AreaToLevel> LoadForAreaToLevel()
        => LoadFor<AreaToLevel>("B3:I69", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<Capsule> LoadForCapsule()
        => LoadFor<Capsule>("B1176:I1868", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Type))
             .MapColumn(_ => _.WithColumnIndex(3).IsRequired().MapTo(_ => _.Number))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<Enemy> LoadForEnemy()
        => LoadFor<Enemy>("B464:I1173", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Type))
             .MapColumn(_ => _.WithColumnIndex(3).IsRequired().MapTo(_ => _.Number))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<FieldEmblem> LoadForFieldEmblem()
        => LoadFor<FieldEmblem>("B370:I382", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Name))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<Fish> LoadForFish()
        => LoadFor<Fish>("B1871:I1894", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Type))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<LevelMission> LoadForLevelMission()
        => LoadFor<LevelMission>("B219:I347", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Mission))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<Mission> LoadForMission()
        => LoadFor<Mission>("B385:I445", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.CardArea))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.ObjectiveArea))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(3).IsRequired().MapTo(_ => _.Number))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<SubGameMission> LoadForSubGameMission()
        => LoadFor<SubGameMission>("B448:I461", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.SubGame))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Mission))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<UpgradeItem> LoadForUpgradeItem()
        => LoadFor<UpgradeItem>("B350:I367", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Upgrade))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseKeyItemLogicRules).MapTo(_ => _.ExpertDXLogic)));

    private static async IAsyncEnumerable<T> LoadFor<T>(string range,
                                                     Func<MappingConfigBuilder<T>, MappingConfigBuilder<T>> mapping) where T : new()
    {
        var adapter = new GoogleSheetAdapter();
        var sheet = await adapter.GetAsync("1MKI-oe2KDodhk1MlMcgdEny0LGvkJETRHZTIyP23Xko",
                                           $"Logic (1.1.0)!{range}",
                                           ApiKey);
        var mapper = new SheetMapper().AddConfigFor(mapping);
        var mapped = mapper.Map<T>(sheet);
        foreach (var model in mapped.ParsedModels)
            yield return model.Value;
    }

    private static LogicRules ParseKeyItemLogicRules(string s)
    {
        s = string.Join(string.Empty, s.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

        var rules = new LogicRules();
        var lines = s.Split("],[", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                     .Select(_ => _.Replace("[", string.Empty).Replace("]", string.Empty));
        foreach (var line in lines)
        {
            var items = line.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            rules.Add([.. items.Select(_ => _.Split('.')[^1])]);
        }
        return rules;
    }
}