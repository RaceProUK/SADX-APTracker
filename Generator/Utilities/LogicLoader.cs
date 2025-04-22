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

    internal static IAsyncEnumerable<AreaToArea> LoadAreaToArea()
        => Load<AreaToArea>("B72:I216", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.AreaFrom))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.AreaTo))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<AreaToLevel> LoadAreaToLevel()
        => Load<AreaToLevel>("B3:I69", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<Boss> LoadBoss()
        => Load<Boss>("B1897:I1915", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Name))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Unified))
             .MapColumn(_ => _.WithColumnIndex(4).IsRequired().MapTo(_ => _.Character)));

    internal static IAsyncEnumerable<Capsule> LoadCapsule()
        => Load<Capsule>("B1176:I1868", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Type))
             .MapColumn(_ => _.WithColumnIndex(3).IsRequired().MapTo(_ => _.Number))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<Enemy> LoadEnemy()
        => Load<Enemy>("B464:I1173", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Type))
             .MapColumn(_ => _.WithColumnIndex(3).IsRequired().MapTo(_ => _.Number))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<FieldEmblem> LoadFieldEmblem()
        => Load<FieldEmblem>("B370:I382", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Name))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<Fish> LoadFish()
        => Load<Fish>("B1871:I1894", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Type))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<LevelMission> LoadLevelMission()
        => Load<LevelMission>("B219:I347", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Mission))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<Mission> LoadMission()
        => Load<Mission>("B385:I445", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.CardArea))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.ObjectiveArea))
             .MapColumn(_ => _.WithColumnIndex(3).IsRequired().MapTo(_ => _.Number))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<SubGameMission> LoadSubGameMission()
        => Load<SubGameMission>("B448:I461", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.SubGame))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Mission))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    internal static IAsyncEnumerable<UpgradeItem> LoadUpgradeItem()
        => Load<UpgradeItem>("B350:I367", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Upgrade))
             .MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(ParseLogicRules).MapTo(_ => _.NormalLogic))
             .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(ParseLogicRules).MapTo(_ => _.HardLogic))
             .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDCLogic))
             .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(ParseLogicRules).MapTo(_ => _.ExpertDXLogic)));

    private static async IAsyncEnumerable<T> Load<T>(string range,
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

    private static IEnumerable<string> ParseLogicRules(string s)
        => s.Split("],[", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => _.Replace("[", string.Empty).Replace("]", string.Empty));
}