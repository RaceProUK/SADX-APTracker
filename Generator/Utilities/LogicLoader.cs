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

    internal static IAsyncEnumerable<AreaToLevel> LoadAreaToLevel()
        => Load<AreaToLevel>("B3:I69", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Level))
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