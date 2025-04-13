using System.Collections.Frozen;
using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class ItemGenerator
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true,
        IndentSize = 4
    };

    private const int CharactersStart = 543800000;
    private const int UpgradesStart = 543800010;
    private const int FillerStart = 543800070;
    private const int Keys1Start = 543800080;
    private const int CollectiblesStart = 543800090;
    private const int GoalsStart = 543800091;
    private const int EmeraldsStart = 543800092;
    private const int TrapsStart = 543800100;
    private const int Keys2Start = 543800120;
    private const int RangeEnd = 543800122;

    internal static async Task Generate(Dictionary<string, int> nameToId)
    {
        var idToName = nameToId.ToFrozenDictionary(_ => _.Value, _ => _.Key);
        await GenerateItemMapLua(idToName);
        await GenerateCharactersJson(idToName);
        await GenerateUpgradesJson(idToName);
        await GenerateKeysJson(idToName);
        await GenerateEmeraldsJson(idToName);
        await GenerateCollectiblesJson(idToName);
        await GenerateGoalsJson(idToName);
    }

    private static async Task GenerateItemMapLua(FrozenDictionary<int, string> idToName)
    {
        var entries = from entry in idToName
                      let parts = entry.Value.Split(' ')
                      let code = string.Join(string.Empty, parts)
                      select $"    [{entry.Key}] = \"{code}\",";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["ItemMap = {", .. entries, "}"]),
            "itemMap.lua", "scripts", "archipelago");
    }

    private static async Task GenerateCharactersJson(FrozenDictionary<int, string> idToName)
    {
        var characters = from entry in idToName
                         where entry.Key >= CharactersStart && entry.Key < UpgradesStart
                         let parts = entry.Value.Split(' ')
                         let img = parts[1]
                         let code = string.Join(string.Empty, parts)
                         select new ToggleItem(entry.Value, $"images/characters/{img}.png", code);
        await FileWriter.WriteFile(JsonSerializer.Serialize(characters, Options), "characters.json", "items");
    }

    private static async Task GenerateUpgradesJson(FrozenDictionary<int, string> idToName)
    {
        var upgrades = from entry in idToName
                       where entry.Key >= UpgradesStart && entry.Key < FillerStart
                       let parts = entry.Value.Split(' ')
                       let code = string.Join(string.Empty, parts)
                       select new ToggleItem(entry.Value, $"images/upgrades/{code}.png", code);
        await FileWriter.WriteFile(JsonSerializer.Serialize(upgrades, Options), "upgrades.json", "items");
    }

    private static async Task GenerateKeysJson(FrozenDictionary<int, string> idToName)
    {
        var keys1 = from entry in idToName
                    where entry.Key >= Keys1Start && entry.Key < CollectiblesStart
                    let parts = entry.Value.Split(' ')
                    let code = string.Join(string.Empty, parts)
                    select new ToggleItem(entry.Value, $"images/keys/{code}.png", code);
        var keys2 = from entry in idToName
                    where entry.Key >= Keys2Start && entry.Key < RangeEnd
                    let parts = entry.Value.Split(' ')
                    let code = string.Join(string.Empty, parts)
                    select new ToggleItem(entry.Value, $"images/keys/{code}.png", code);
        var keys = keys1.Union(keys2);
        await FileWriter.WriteFile(JsonSerializer.Serialize(keys, Options), "keys.json", "items");
    }

    private static async Task GenerateEmeraldsJson(FrozenDictionary<int, string> idToName)
    {
        var emeralds = from entry in idToName
                       where entry.Key >= EmeraldsStart && entry.Key < TrapsStart
                       let parts = entry.Value.Split(' ')
                       let img = parts[0]
                       let code = string.Join(string.Empty, parts)
                       select new ToggleItem(entry.Value, $"images/emeralds/{img}.png", code);
        await FileWriter.WriteFile(JsonSerializer.Serialize(emeralds, Options), "emeralds.json", "items");
    }

    private static async Task GenerateCollectiblesJson(FrozenDictionary<int, string> idToName)
    {
        var collectibles = from entry in idToName
                           where entry.Key >= CollectiblesStart && entry.Key < GoalsStart
                           let parts = entry.Value.Split(' ')
                           let code = string.Join(string.Empty, parts)
                           select new CollectibleItem(entry.Value, $"images/collectibles/{code}.png", code, 130);
        await FileWriter.WriteFile(JsonSerializer.Serialize(collectibles, Options), "collectibles.json", "items");
    }

    private static async Task GenerateGoalsJson(FrozenDictionary<int, string> idToName)
    {
        var goals = from entry in idToName
                    where entry.Key >= GoalsStart && entry.Key < EmeraldsStart
                    let parts = entry.Value.Split(' ')
                    let code = string.Join(string.Empty, parts)
                    select new ToggleItem(entry.Value, $"images/goals/{code}.png", code);
        await FileWriter.WriteFile(JsonSerializer.Serialize(goals, Options), "goals.json", "items");
    }
}