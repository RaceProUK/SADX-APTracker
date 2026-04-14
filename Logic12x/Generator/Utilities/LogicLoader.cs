using System.Text.RegularExpressions;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Humanizer;
using Microsoft.Extensions.Configuration;
using RPS.SADX.PopTracker.Generator.Models.Logic;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class LogicLoader
{
    private static string ApiKey { get; }

    static LogicLoader()
    {
        var config = new ConfigurationBuilder().AddUserSecrets("b142e81d-996d-4b19-a68a-38a41c45604e").Build();
        ApiKey = config["GoogleApiKey"]!;
    }

    internal static IAsyncEnumerable<Connection> LoadForConnections() => LoadFor<Connection>("B3:J573", row => new()
    {
        Character = row.Values[0].EffectiveValue.StringValue,
        AreaFrom = ParseAreaName(row.Values[1].EffectiveValue?.StringValue ?? string.Empty),
        AreaTo = ParseAreaName(row.Values[2].EffectiveValue.StringValue),
        Tag = row.Values[3].EffectiveValue?.StringValue,
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].EffectiveValue?.StringValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].EffectiveValue?.StringValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].EffectiveValue?.StringValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].EffectiveValue?.StringValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].EffectiveValue?.StringValue),
    });

    internal static IAsyncEnumerable<Capsule> LoadForCapsule() => LoadFor<Capsule>("B1534:J2226", row => new()
    {
        Level = ParseAreaName(row.Values[0].EffectiveValue.StringValue),
        Character = row.Values[1].EffectiveValue.StringValue,
        Type = row.Values[2].EffectiveValue.StringValue,
        Number = Convert.ToInt32(row.Values[3].EffectiveValue.NumberValue),
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].EffectiveValue?.StringValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].EffectiveValue?.StringValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].EffectiveValue?.StringValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].EffectiveValue?.StringValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].EffectiveValue?.StringValue),
    });

    internal static IAsyncEnumerable<Enemy> LoadForEnemy() => LoadFor<Enemy>("B822:J1531", row => new()
    {
        Level = ParseAreaName(row.Values[0].EffectiveValue.StringValue),
        Character = row.Values[1].EffectiveValue.StringValue,
        Type = row.Values[2].EffectiveValue.StringValue,
        Number = Convert.ToInt32(row.Values[3].EffectiveValue.NumberValue),
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].EffectiveValue?.StringValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].EffectiveValue?.StringValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].EffectiveValue?.StringValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].EffectiveValue?.StringValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].EffectiveValue?.StringValue),
    });

    internal static IAsyncEnumerable<FieldEmblem> LoadForFieldEmblem() => LoadFor<FieldEmblem>("B728:J740", row => new()
    {
        Area = ParseAreaName(row.Values[0].EffectiveValue.StringValue),
        Name = row.Values[1].EffectiveValue.StringValue,
        NormalLogic = ParseEmblemLogicRules(row.Values[4].EffectiveValue?.StringValue),
        HardLogic = ParseEmblemLogicRules(row.Values[5].EffectiveValue?.StringValue),
        ExpertDCLogic = ParseEmblemLogicRules(row.Values[6].EffectiveValue?.StringValue),
        ExpertDXLogic = ParseEmblemLogicRules(row.Values[7].EffectiveValue?.StringValue),
        ExpertDXPlusLogic = ParseEmblemLogicRules(row.Values[8].EffectiveValue?.StringValue),
    });

    internal static IAsyncEnumerable<Fish> LoadForFish() => LoadFor<Fish>("B2229:J2252", row => new()
    {
        Level = ParseAreaName(row.Values[0].EffectiveValue.StringValue),
        Type = row.Values[1].EffectiveValue.StringValue,
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].EffectiveValue?.StringValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].EffectiveValue?.StringValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].EffectiveValue?.StringValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].EffectiveValue?.StringValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].EffectiveValue?.StringValue),
    });

    internal static IAsyncEnumerable<LevelMission> LoadForLevelMission() => LoadFor<LevelMission>("B577:J705", row => new()
    {
        Level = ParseAreaName(row.Values[0].EffectiveValue.StringValue),
        Character = row.Values[1].EffectiveValue.StringValue,
        Mission = row.Values[2].EffectiveValue.StringValue,
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].EffectiveValue?.StringValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].EffectiveValue?.StringValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].EffectiveValue?.StringValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].EffectiveValue?.StringValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].EffectiveValue?.StringValue),
    });

    internal static IAsyncEnumerable<Mission> LoadForMission() => LoadFor<Mission>("B743:J803", row => new()
    {
        CardArea = ParseAreaName(row.Values[0].EffectiveValue.StringValue),
        ObjectiveArea = ParseAreaName(row.Values[1].EffectiveValue.StringValue),
        Character = row.Values[2].EffectiveValue.StringValue,
        Number = Convert.ToInt32(row.Values[3].EffectiveValue.NumberValue),
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].EffectiveValue?.StringValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].EffectiveValue?.StringValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].EffectiveValue?.StringValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].EffectiveValue?.StringValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].EffectiveValue?.StringValue),
    });

    internal static IAsyncEnumerable<UpgradeItem> LoadForUpgradeItem() => LoadFor<UpgradeItem>("B708:J725", row => new()
    {
        Area = ParseAreaName(row.Values[0].EffectiveValue.StringValue),
        Character = row.Values[1].EffectiveValue.StringValue,
        Upgrade = row.Values[2].EffectiveValue.StringValue,
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].EffectiveValue?.StringValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].EffectiveValue?.StringValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].EffectiveValue?.StringValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].EffectiveValue?.StringValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].EffectiveValue?.StringValue),
    });

    private static async IAsyncEnumerable<T> LoadFor<T>(string range, Func<RowData, T> mapper) where T : new()
    {
        var service = new SheetsService(new BaseClientService.Initializer
        {
            ApplicationName = "SADX PopTracker Generator",
            ApiKey = ApiKey
        });
        var request = service.Spreadsheets.Get("1XTdY4A6WUXBDqCwr2n7fuOTlDFJUs5o82pKRkiQ5vic");
        request.IncludeGridData = true;
        request.Ranges = $"Logic (1.2.1)!{range}";

        var response = await request.ExecuteAsync();
        foreach (var row in response.Sheets[0].Data[0].RowData.Skip(1))
        {
            yield return mapper(row);
        }
    }

    private static string ParseAreaName(string s) => s.Pascalize();

    private static LogicRules ParseKeyItemLogicRules(string? s)
    {
        if (s is null) return [];

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

    private static LogicRules ParseEmblemLogicRules(string? s)
    {
        if (s is null) return [];

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