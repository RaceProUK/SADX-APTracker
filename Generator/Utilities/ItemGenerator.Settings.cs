using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class ItemGenerator
{
    private static readonly IEnumerable<string> Characters = ["Sonic", "Tails", "Knuckles", "Amy", "Gamma", "Big"];

    private static readonly IEnumerable<char> Missions = ['C', 'B', 'A', 'S'];

    private static readonly IEnumerable<string> Starts =
    [
        "Station Square (Hub)",
        "Station",
        "Hotel",
        "Casino",
        "Twinkle Park Lobby",
        "Mystic Ruins (Hub)",
        "Angel Island",
        "Jungle",
        "Egg Carrier Outside",
        "Egg Carrier Inside",
        "Egg Carrier Front Deck"
    ];

    private static async Task GenerateSettingsJson()
    {
        var settings = GenerateGoalSettings().Concat(GenerateLogicSettings())
                                             .Concat(GeneratePlayableCharacterSettings())
                                             .Concat(GenerateStartingLocationSettings())
                                             .Concat(GenerateLevelMissionSettings())
                                             .Concat(GenerateSubGameSettings())
                                             .Concat(GenerateSanitySettings())
                                             .Concat(GenerateMissionsSettings());
        await FileWriter.WriteFile(JsonSerializer.Serialize(settings, Constants.JsonOptions),
                                   "settings.json",
                                   "items");
    }

    private static string MakeImgPath(string section, string img) => $"images/settings/{section}/{img}.png";

    private static string ToPossessive(this string word) => word[^1] == 's' ? $"{word}'" : $"{word}'s";

    private static IEnumerable<Item> GenerateGoalSettings()
    {
        yield return new CollectibleItem("Emblems Required", "EmblemsRequired", MakeImgPath("goals", "Emblems"), 1500);
        yield return new CollectibleItem("Levels Required", "LevelsRequired", MakeImgPath("goals", "Levels"), 32);
        yield return new ToggleItem("Chaos Emeralds Required", "EmeraldsRequired", MakeImgPath("goals", "Emeralds"));
        yield return new CollectibleItem("Bosses Required", "BossesRequired", MakeImgPath("goals", "Bosses"), 15);
        yield return new CollectibleItem("Missions Required", "MissionsRequired", MakeImgPath("goals", "Missions"), 60);
        yield return new ToggleItem("Chao Races Required", "ChaoRacesRequired", MakeImgPath("goals", "ChaoRaces"));
        yield return new CollectibleItem("Levels Required for Chao Races", "ChaoRacesAccessLevels", MakeImgPath("goals", "ChaoRacesAccessLevels"), 128);
    }

    private static IEnumerable<Item> GenerateLogicSettings()
    {
        yield return new ProgressiveItem("Logic Level", "LogicLevel", true, false, 0,
        [
            new ProgressiveItemStage("Normal Logic", "NormalLogic", MakeImgPath("logic", "NormalLogic"), false),
            new ProgressiveItemStage("Hard Logic", "HardLogic", MakeImgPath("logic", "HardLogic"), false),
            new ProgressiveItemStage("Expert DC Logic", "ExpertLogicDC", MakeImgPath("logic", "ExpertLogicDC"), false),
            new ProgressiveItemStage("Expert DX Logic", "ExpertLogicDX", MakeImgPath("logic", "ExpertLogicDX"), false),
            new ProgressiveItemStage("Expert DX+ Logic", "ExpertLogicDXPlus", MakeImgPath("logic", "ExpertLogicDXPlus"), false)
        ]);
        yield return new ProgressiveItem("Lazy Fishing", "LazyFishing", true, false, 0,
        [
            new ProgressiveItemStage("Lazy Fishing - Off", "LazyFishingOff", MakeImgPath("logic", "LazyFishingOff"), false),
            new ProgressiveItemStage("Lazy Fishing - No Logic", "LazyFishingNoLogic", MakeImgPath("logic", "LazyFishingNoLogic"), false),
            new ProgressiveItemStage("Lazy Fishing - Fishsanity", "LazyFishingFishsanity", MakeImgPath("logic", "LazyFishingFishsanity"), false),
            new ProgressiveItemStage("Lazy Fishing - All", "LazyFishingAll", MakeImgPath("logic", "LazyFishingAll"), false)
        ]);

        yield return new ToggleItem("Include Field Emblem Checks", "FieldEmblemChecks", MakeImgPath("logic", "FieldEmblemChecks"));
        yield return new ToggleItem("Include Mission Mode Checks", "MissionModeChecks", MakeImgPath("logic", "MissionModeChecks"));
        yield return new ToggleItem("Auto-start Missions", "AutoStartMissions", MakeImgPath("logic", "AutoStartMissions"));
        yield return new ToggleItem("Include Secret Chao Eggs", "SecretChaoEggs", MakeImgPath("logic", "SecretChaoEggs"));
        yield return new ToggleItem("Include Chao Races", "ChaoRacesChecks", MakeImgPath("logic", "ChaoRacesChecks"));

        yield return new ToggleItem("Unify Egg Hornet", "UnifyEggHornet", MakeImgPath("logic", "UnifyEggHornet"));
        yield return new ToggleItem("Unify Chaos 4", "UnifyChaos4", MakeImgPath("logic", "UnifyChaos4"));
        yield return new ToggleItem("Unify Chaos 6", "UnifyChaos6", MakeImgPath("logic", "UnifyChaos6"));
    }

    private static IEnumerable<Item> GeneratePlayableCharacterSettings()
        => Characters.Select(_ => new ToggleItem($"{_} Playable",
                                                 $"{_}Playable",
                                                 MakeImgPath("characters", $"{_}Playable")));

    private static IEnumerable<Item> GenerateStartingLocationSettings()
        => from character in Characters
           select new ProgressiveItem($"{character.ToPossessive()} Starting Location",
                                      $"{character}Start",
                                      true,
                                      false,
                                      0,
                                      from start in Starts
                                      let code = Common.RemoveWhitespace(start).Replace("(Hub)", "Main")
                                      select new ProgressiveItemStage(start,
                                                                      $"{character}{code}",
                                                                      MakeImgPath("starts", code),
                                                                      false));

    private static IEnumerable<Item> GenerateLevelMissionSettings()
        => from character in Characters
           select new ProgressiveItem($"{character} Missions",
                                      $"{character}Missions",
                                      true,
                                      true,
                                      0,
                                      from mission in Missions
                                      select new ProgressiveItemStage($"{character.ToPossessive()} {mission} Missions",
                                                                      $"{character}Mission{mission}",
                                                                      MakeImgPath("objectives", $"{character}Mission{mission}")));

    private static IEnumerable<Item> GenerateSubGameSettings()
    {
        yield return new ProgressiveItem("Sky Chase Checks", "SkyChaseChecks", true, true, 0,
        [
            new ProgressiveItemStage("Sky Chase - Normal Mission Only", "EnableSkyChase", MakeImgPath("objectives", "SkyChaseB")),
            new ProgressiveItemStage("Sky Chase - Both Missions", "EnableSkyChaseHard", MakeImgPath("objectives", "SkyChaseA"))
        ]);
        yield return new ProgressiveItem("Sand Hill Checks", "SandHillChecks", true, true, 0,
        [
            new ProgressiveItemStage("Sand Hill - Normal Mission Only", "EnableSandHill", MakeImgPath("objectives", "SandHillB")),
            new ProgressiveItemStage("Sand Hill - Both Missions", "EnableSandHillHard", MakeImgPath("objectives", "SandHillA"))
        ]);
        yield return new ProgressiveItem("Twinkle Circuit Checks", "TwinkleCircuitChecks", true, true, 0,
        [
            new ProgressiveItemStage("Twinkle Circuit - Single Check", "EnableTwinkleCircuit", MakeImgPath("objectives", "TwinkleCircuit"), false),
            new ProgressiveItemStage("Twinkle Circuit - Character Checks", "EnableTwinkleCircuitMultiple", MakeImgPath("objectives", "TwinkleCircuitMultiple"), false)
        ]);
    }

    private static IEnumerable<Item> GenerateSanitySettings()
    {
        yield return new ToggleItem("Enemysanity", "Enemysanity", MakeImgPath("sanities", "Enemysanity"));
        foreach (var i in Characters.Select(_ => new ToggleItem($"{_.ToPossessive()} Enemysanity",
                                                                $"{_}Enemysanity",
                                                                MakeImgPath("sanities", $"{_}Enemysanity"))))
            yield return i;

        yield return new ToggleItem("Capsulesanity", "Capsulesanity", MakeImgPath("sanities", "Capsulesanity"));
        foreach (var i in Characters.Select(_ => new ToggleItem($"{_.ToPossessive()} Capsulesanity",
                                                                $"{_}Capsulesanity",
                                                                MakeImgPath("sanities", $"{_}Capsulesanity"))))
            yield return i;

        yield return new ToggleItem("Pinball Capsules", "PinballCapsules", MakeImgPath("sanities", "PinballCapsules"));
        yield return new ToggleItem("Life Capsulesanity", "LifeCapsulesanity", MakeImgPath("sanities", "LifeCapsulesanity"));
        yield return new ToggleItem("Shield Capsulesanity", "ShieldCapsulesanity", MakeImgPath("sanities", "ShieldCapsulesanity"));
        yield return new ToggleItem("Power-up Capsulesanity", "PowerupCapsulesanity", MakeImgPath("sanities", "PowerupCapsulesanity"));
        yield return new ToggleItem("Ring Capsulesanity", "RingCapsulesanity", MakeImgPath("sanities", "RingCapsulesanity"));

        yield return new ToggleItem("Fishsanity", "Fishsanity", MakeImgPath("sanities", "Fishsanity"));
    }

    private static IEnumerable<Item> GenerateMissionsSettings()
        => Enumerable.Range(1, 60)
                     .Select(_ => new ToggleItem($"Allow Mission {_}",
                                                 $"AllowMission{_}",
                                                 MakeImgPath("goals", "Missions")));
}