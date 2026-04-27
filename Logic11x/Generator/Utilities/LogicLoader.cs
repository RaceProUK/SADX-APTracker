using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Configuration;
using RPS.SADX.PopTracker.Generator.Models.Logic;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class LogicLoader
{
    private static string ApiKey { get; }

    static LogicLoader()
    {
        var config = new ConfigurationBuilder().AddUserSecrets("b142e81d-996d-4b19-a68a-38a41c45604e").Build();
        ApiKey = config["GoogleApiKey"]!;
    }

    internal static IAsyncEnumerable<AreaToArea> LoadForAreaToArea() => LoadFor<AreaToArea>("B72:J216", row => new()
    {
        Character = row.Values[0].FormattedValue,
        AreaFrom = row.Values[1].FormattedValue,
        AreaTo = row.Values[2].FormattedValue,
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].FormattedValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].FormattedValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].FormattedValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].FormattedValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].FormattedValue),
    });

    internal static IAsyncEnumerable<AreaToLevel> LoadForAreaToLevel() => LoadFor<AreaToLevel>("B3:J69", row => new()
    {
        Character = row.Values[0].FormattedValue,
        Area = row.Values[1].FormattedValue,
        Level = row.Values[2].FormattedValue,
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].FormattedValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].FormattedValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].FormattedValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].FormattedValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].FormattedValue),
    });

    internal static IAsyncEnumerable<Capsule> LoadForCapsule() => LoadFor<Capsule>("B1176:J1868", row => new()
    {
        Level = row.Values[0].FormattedValue,
        Character = row.Values[1].FormattedValue,
        Type = row.Values[2].FormattedValue,
        Number = Convert.ToInt32(row.Values[3].EffectiveValue.NumberValue),
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].FormattedValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].FormattedValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].FormattedValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].FormattedValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].FormattedValue),
    });

    internal static IAsyncEnumerable<Enemy> LoadForEnemy() => LoadFor<Enemy>("B464:J1173", row => new()
    {
        Level = row.Values[0].FormattedValue,
        Character = row.Values[1].FormattedValue,
        Type = row.Values[2].FormattedValue,
        Number = Convert.ToInt32(row.Values[3].EffectiveValue.NumberValue),
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].FormattedValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].FormattedValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].FormattedValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].FormattedValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].FormattedValue),
    });

    internal static IAsyncEnumerable<FieldEmblem> LoadForFieldEmblem() => LoadFor<FieldEmblem>("B370:J382", row => new()
    {
        Area = row.Values[0].FormattedValue,
        Name = row.Values[1].FormattedValue,
        NormalLogic = ParseCharacterLogicRules(row.Values[4].FormattedValue),
        HardLogic = ParseCharacterLogicRules(row.Values[5].FormattedValue),
        ExpertDCLogic = ParseCharacterLogicRules(row.Values[6].FormattedValue),
        ExpertDXLogic = ParseCharacterLogicRules(row.Values[7].FormattedValue),
        ExpertDXPlusLogic = ParseCharacterLogicRules(row.Values[8].FormattedValue),
    });

    internal static IAsyncEnumerable<Fish> LoadForFish() => LoadFor<Fish>("B1871:J1894", row => new()
    {
        Level = row.Values[0].FormattedValue,
        Type = row.Values[1].FormattedValue,
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].FormattedValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].FormattedValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].FormattedValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].FormattedValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].FormattedValue),
    });

    internal static IAsyncEnumerable<LevelMission> LoadForLevelMission() => LoadFor<LevelMission>("B219:J347", row => new()
    {
        Level = row.Values[0].FormattedValue,
        Character = row.Values[1].FormattedValue,
        Mission = row.Values[2].FormattedValue,
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].FormattedValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].FormattedValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].FormattedValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].FormattedValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].FormattedValue),
    });

    internal static IAsyncEnumerable<Mission> LoadForMission() => LoadFor<Mission>("B385:J445", row => new()
    {
        CardArea = row.Values[0].FormattedValue,
        ObjectiveArea = row.Values[1].FormattedValue,
        Character = row.Values[2].FormattedValue,
        Number = Convert.ToInt32(row.Values[3].EffectiveValue.NumberValue),
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].FormattedValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].FormattedValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].FormattedValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].FormattedValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].FormattedValue),
    });

    internal static IAsyncEnumerable<UpgradeItem> LoadForUpgradeItem() => LoadFor<UpgradeItem>("B350:J367", row => new()
    {
        Area = row.Values[0].FormattedValue,
        Character = row.Values[1].FormattedValue,
        Upgrade = row.Values[2].FormattedValue,
        NormalLogic = ParseKeyItemLogicRules(row.Values[4].FormattedValue),
        HardLogic = ParseKeyItemLogicRules(row.Values[5].FormattedValue),
        ExpertDCLogic = ParseKeyItemLogicRules(row.Values[6].FormattedValue),
        ExpertDXLogic = ParseKeyItemLogicRules(row.Values[7].FormattedValue),
        ExpertDXPlusLogic = ParseKeyItemLogicRules(row.Values[8].FormattedValue),
    });

    private static async IAsyncEnumerable<T> LoadFor<T>(string range, Func<RowData, T> mapper) where T : new()
    {
        var service = new SheetsService(new BaseClientService.Initializer
        {
            ApplicationName = "SADX PopTracker Generator",
            ApiKey = ApiKey
        });
        var request = service.Spreadsheets.Get("1MKI-oe2KDodhk1MlMcgdEny0LGvkJETRHZTIyP23Xko");
        request.IncludeGridData = true;
        request.Ranges = $"Logic (1.1.2)!{range}";

        var response = await request.ExecuteAsync();
        foreach (var row in response.Sheets[0].Data[0].RowData.Skip(1))
        {
            yield return mapper(row);
        }
    }

    private static LogicRules ParseCharacterLogicRules(string s)
    {
        s = Common.RemoveWhitespace(s);

        var rules = new LogicRules();
        var lines = s.Split(",C", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            if (line.Contains("Upgrade"))
            {
                var start = line.IndexOf('(') + 1;
                var parts = line[start..^1].Split(',');
                var character = parts[0].Split('.')[^1];
                var item = parts[1].Split('.')[^1];
                rules.Add([$"Playable{character}", item]);
            }
            else
            {
                var character = line.Split('.')[^1];
                rules.Add([$"Playable{character}"]);
            }
        }
        return rules;
    }

    private static LogicRules ParseKeyItemLogicRules(string s)
    {
        if (s is null) return [];

        s = Common.RemoveWhitespace(s);

        var rules = new LogicRules();
        var lines = s.Split("],[", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                     .Select(_ => _.Replace("[", string.Empty).Replace("]", string.Empty));
        foreach (var line in lines)
        {
            var items = line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            rules.Add([.. items.Select(_ => _.Split('.')[^1])]);
        }
        return rules;
    }
}