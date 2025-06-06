﻿using System.Collections.Frozen;
using System.Text.Json;
using Humanizer;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class ItemGenerator
{
    private const int CharactersStart = 543800000;
    private const int UpgradesStart = 543800010;
    private const int FillerStart = 543800070;
    private const int Keys1Start = 543800080;
    private const int CollectiblesStart = 543800090;
    private const int GoalsStart = 543800091;
    private const int EmeraldsStart = 543800092;
    private const int TrapsStart = 543800100;
    private const int Keys2Start = 543800120;
    private const int Keys2End = 543800125;

    internal static async Task Generate(IDictionary<string, int> dict)
    {
        var idToName = dict.ToFrozenDictionary(_ => _.Value, _ => _.Key);
        await GenerateItemMapLua(idToName);
        await GenerateCharactersJson(idToName);
        await GenerateUpgradesJson(idToName);
        await GenerateKeysJson(idToName);
        await GenerateEmeraldsJson(idToName);
        await GenerateProgressionJson(idToName);
        await GenerateGoalsJson(idToName);
        await GenerateSettingsJson();
    }

    private static string GetWord(string name, int index) => name.Split(' ')[index];

    private static string MakeCode(string name) => string.Join(string.Empty, name.Split(' '));

    private static async Task GenerateItemMapLua(FrozenDictionary<int, string> dict)
    {
        var entries = from entry in dict
                      let code = MakeCode(entry.Value)
                      select $"    [{entry.Key}] = \"{code}\",";
        await FileWriter.WriteFile(string.Join(Environment.NewLine, ["ItemMap = {", .. entries, "}"]),
                                   "itemMap.lua",
                                   "scripts",
                                   "archipelago");
    }

    private static async Task GenerateCharactersJson(FrozenDictionary<int, string> dict)
    {
        var characters = from entry in dict
                         where entry.Key >= CharactersStart && entry.Key < UpgradesStart
                         let img = GetWord(entry.Value, 1)
                         let code = MakeCode(entry.Value)
                         select new ToggleItem(entry.Value, code, $"images/characters/{img}.png");
        await FileWriter.WriteFile(JsonSerializer.Serialize(characters, Constants.JsonOptions),
                                   "characters.json",
                                   "items");
    }

    private static async Task GenerateUpgradesJson(FrozenDictionary<int, string> dict)
    {
        var upgrades = from entry in dict
                       where entry.Key >= UpgradesStart && entry.Key < FillerStart
                       let code = MakeCode(entry.Value)
                       select new ToggleItem(entry.Value, code, $"images/upgrades/{code}.png");
        await FileWriter.WriteFile(JsonSerializer.Serialize(upgrades, Constants.JsonOptions),
                                   "upgrades.json",
                                   "items");
    }

    private static async Task GenerateKeysJson(FrozenDictionary<int, string> dict)
    {
        var keys1 = from entry in dict
                    where entry.Key >= Keys1Start && entry.Key < CollectiblesStart
                    let code = MakeCode(entry.Value)
                    select new ToggleItem(entry.Value, code, $"images/keys/{code}.png");
        var keys2 = from entry in dict
                    where entry.Key >= Keys2Start && entry.Key < Keys2End
                    let code = MakeCode(entry.Value)
                    select new ToggleItem(entry.Value, code, $"images/keys/{code}.png");
        var keys = keys1.Union(keys2);
        await FileWriter.WriteFile(JsonSerializer.Serialize(keys, Constants.JsonOptions),
                                   "keys.json",
                                   "items");
    }

    private static async Task GenerateEmeraldsJson(FrozenDictionary<int, string> dict)
    {
        var emeralds = from entry in dict
                       where entry.Key >= EmeraldsStart && entry.Key < TrapsStart
                       let img = GetWord(entry.Value, 0)
                       let code = MakeCode(entry.Value)
                       select new ToggleItem(entry.Value, code, $"images/emeralds/{img}.png");
        await FileWriter.WriteFile(JsonSerializer.Serialize(emeralds, Constants.JsonOptions),
                                   "emeralds.json",
                                   "items");
    }

    private static async Task GenerateProgressionJson(FrozenDictionary<int, string> dict)
    {
        var progressionItems = from entry in dict
                               where entry.Key >= CollectiblesStart && entry.Key < GoalsStart
                               let code = MakeCode(entry.Value).Pluralize()
                               select new CollectibleItem(entry.Value.Pluralize(),
                                                          code,
                                                          $"images/progression/{code}.png",
                                                          1500);
        var levelsBeaten = new CollectibleItem("Levels Beaten",
                                               "LevelsBeaten",
                                               $"images/progression/Levels.png",
                                               32);
        var bossesBeaten = new CollectibleItem("Bosses Beaten",
                                               "BossesBeaten",
                                               $"images/progression/Bosses.png",
                                               15);
        var missionsBeaten = new CollectibleItem("Missions Beaten",
                                                 "MissionsBeaten",
                                                 $"images/progression/Missions.png",
                                                 60);
        var chaoRacesWon = new CollectibleItem("Chao Races Won",
                                               "ChaoRacesWon",
                                               $"images/progression/ChaoRaces.png",
                                               5);
        progressionItems = [.. progressionItems, levelsBeaten, bossesBeaten, missionsBeaten, chaoRacesWon];
        await FileWriter.WriteFile(JsonSerializer.Serialize(progressionItems, Constants.JsonOptions),
                                   "progression.json",
                                   "items");
    }

    private static async Task GenerateGoalsJson(FrozenDictionary<int, string> dict)
    {
        var goals = from entry in dict
                    where entry.Key >= GoalsStart && entry.Key < EmeraldsStart
                    let code = MakeCode(entry.Value)
                    select new ToggleItem(entry.Value, code, $"images/goals/{code}.png");
        await FileWriter.WriteFile(JsonSerializer.Serialize(goals, Constants.JsonOptions),
                                   "goals.json",
                                   "items");
    }
}