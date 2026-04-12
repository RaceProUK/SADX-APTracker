using System.Text.RegularExpressions;
using Humanizer;
using Microsoft.Extensions.Configuration;
using RPS.SADX.PopTracker.Generator.Models.Logic;
using SheetToObjects.Adapters.GoogleSheets;
using SheetToObjects.Lib;
using SheetToObjects.Lib.FluentConfiguration;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LogicLoader
{
    private static string ApiKey { get; }

    static LogicLoader()
    {
        var config = new ConfigurationBuilder().AddUserSecrets("b142e81d-996d-4b19-a68a-38a41c45604e").Build();
        ApiKey = config["GoogleApiKey"]!;
    }

    internal static IAsyncEnumerable<Capsule> LoadForCapsule()
        => LoadFor<Capsule>("B1534:J2226", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Type))
             .MapColumn(_ => _.WithColumnIndex(3).IsRequired().MapTo(_ => _.Number))
             .MapCommonColumns(ParseKeyItemLogicRules));

    internal static IAsyncEnumerable<Enemy> LoadForEnemy()
        => LoadFor<Enemy>("B822:J1531", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Type))
             .MapColumn(_ => _.WithColumnIndex(3).IsRequired().MapTo(_ => _.Number))
             .MapCommonColumns(ParseKeyItemLogicRules));

    internal static IAsyncEnumerable<FieldEmblem> LoadForFieldEmblem()
        => LoadFor<FieldEmblem>("B728:J740", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().ParseValueUsing(ParseAreaName).MapTo(_ => _.Area))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Name))
             .MapCommonColumns(ParseEmblemLogicRules));

    internal static IAsyncEnumerable<Fish> LoadForFish()
        => LoadFor<Fish>("B2229:J2252", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Type))
             .MapCommonColumns(ParseKeyItemLogicRules));

    internal static IAsyncEnumerable<LevelMission> LoadForLevelMission()
        => LoadFor<LevelMission>("B577:J705", _ =>
            _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().ParseValueUsing(ParseAreaName).MapTo(_ => _.Level))
             .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Character))
             .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Mission))
             .MapCommonColumns(ParseKeyItemLogicRules));

    internal static IAsyncEnumerable<UpgradeItem> LoadForUpgradeItem()
         => LoadFor<UpgradeItem>("B708:J725", _ =>
             _.MapColumn(_ => _.WithColumnIndex(0).IsRequired().ParseValueUsing(ParseAreaName).MapTo(_ => _.Area))
              .MapColumn(_ => _.WithColumnIndex(1).IsRequired().MapTo(_ => _.Character))
              .MapColumn(_ => _.WithColumnIndex(2).IsRequired().MapTo(_ => _.Upgrade))
              .MapCommonColumns(ParseKeyItemLogicRules));

    private static MappingConfigBuilder<T> MapCommonColumns<T>(this MappingConfigBuilder<T> builder, Func<string, object> valueParser)
        where T : LogicSpecification
        => builder.MapColumn(_ => _.WithColumnIndex(4).ParseValueUsing(valueParser).MapTo(_ => _.NormalLogic))
                  .MapColumn(_ => _.WithColumnIndex(5).ParseValueUsing(valueParser).MapTo(_ => _.HardLogic))
                  .MapColumn(_ => _.WithColumnIndex(6).ParseValueUsing(valueParser).MapTo(_ => _.ExpertDCLogic))
                  .MapColumn(_ => _.WithColumnIndex(7).ParseValueUsing(valueParser).MapTo(_ => _.ExpertDXLogic))
                  .MapColumn(_ => _.WithColumnIndex(8).ParseValueUsing(valueParser).MapTo(_ => _.ExpertDXPlusLogic));

    private static async IAsyncEnumerable<T> LoadFor<T>(string range,
                                                        Func<MappingConfigBuilder<T>, MappingConfigBuilder<T>> mapping) where T : new()
    {
        var adapter = new GoogleSheetAdapter();
        var sheet = await adapter.GetAsync("1XTdY4A6WUXBDqCwr2n7fuOTlDFJUs5o82pKRkiQ5vic",
                                           $"Logic (1.2.1)!{range}",
                                           ApiKey);
        var mapper = new SheetMapper().AddConfigFor(mapping);
        var mapped = mapper.Map<T>(sheet);
        foreach (var model in mapped.ParsedModels)
        {
            yield return model.Value;
        }
    }

    private static string ParseAreaName(string s) => s.Pascalize();

    private static LogicRules ParseKeyItemLogicRules(string s)
    {
        s = Common.RemoveWhitespace(s).Pascalize().Replace("AllLures", "Lure1,Lure2,Lure3,Lure4");

        var rules = new LogicRules();
        var lines = s.Split("],[", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                     .Select(_ => _.Replace("[", string.Empty).Replace("]", string.Empty));
        foreach (var line in lines)
        {
            rules.Add([.. line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)]);
        }
        return rules;
    }

    private static LogicRules ParseEmblemLogicRules(string s)
    {
        s = Common.RemoveWhitespace(s);

        var rules = new LogicRules();
        var lines = s.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var parsedLogic = EmblemLogicParser().Match(line);
            var character = parsedLogic.Groups["character"].Value.Transform(To.LowerCase, To.TitleCase);
            var upgrade = parsedLogic.Groups["upgrade"].Value;
            var parts = new List<string> { $"Playable{character}" };
            if (!string.IsNullOrEmpty(upgrade))
            {
                parts.Add(upgrade switch
                {
                    "JB" => "JetBooster",
                    "LS" => "LightShoes",
                    "SC" => "ShovelClaw",
                    _ => throw new InvalidCastException(nameof(upgrade))
                });
            }
            rules.Add([.. parts]);
        }
        return rules;
    }

    [GeneratedRegex("P_(?<character>[A-Za-z]+)(_W_(?<upgrade>[A-Za-z]+))?")]
    private static partial Regex EmblemLogicParser();
}