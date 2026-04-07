using System.Text.Json;
using Humanizer;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class ItemGenerator
{
    private const string GoalsFolder = "goals";
    private const string LogicFolder = "logic";
    private const string CharactersFolder = "characters";
    private const string ObjectivesFolder = "objectives";
    private const string SanitiesFolder = "sanities";

    private static readonly IEnumerable<string> Characters = ["Sonic", "Tails", "Knuckles", "Amy", "Gamma", "Big"];

    private static readonly IEnumerable<char> Missions = ['C', 'B', 'A', 'S'];

    private static readonly IEnumerable<string> Starts =
    [
        "Station Square - City Hall",
        "Station Square - Station",
        "Station Square - Casino",
        "Station Square - Sewers",
        "Station Square - Hub",
        "Station Square - Twinkle Park Tunnel",
        "Station Square - Hotel",
        "Station Square - Hotel Pool",
        "Station Square - Twinkle Park Lobby",
        "Mystic Ruins - Hub",
        "Mystic Ruins - Angel Island",
        "Mystic Ruins - Ice Cave",
        "Mystic Ruins - Past Altar",
        "Mystic Ruins - Past City",
        "Mystic Ruins - Jungle",
        "Mystic Ruins - Final Egg Tower",
        "Egg Carrier - Outside",
        "Egg Carrier - Bridge",
        "Egg Carrier - Deck",
        "Egg Carrier - Captain Room",
        "Egg Carrier - Pool",
        "Egg Carrier - Arsenal",
        "Egg Carrier - Inside",
        "Egg Carrier - Hedgehog Hammer",
        "Egg Carrier - Prison Hall",
        "Egg Carrier - Water Tank",
        "Egg Carrier - Warp Hall"
    ];

    private static readonly IEnumerable<string> Capsules = ["Life", "Shield", "Power-up", "Ring"];

    private static async Task GenerateSettingsJson()
    {
        var settings = GenerateHiddenSettings().Concat(GenerateGoalSettings())
                                               .Concat(GenerateLogicSettings())
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

    private static IEnumerable<Item> GenerateHiddenSettings()
    {
        yield return new HiddenItem("Logic Version Mismatch", "VersionMismatch");
    }

    private static IEnumerable<Item> GenerateGoalSettings()
    {
        yield return new CollectibleItem("Emblems Required", "EmblemsRequired", MakeImgPath(GoalsFolder, "Emblems"), 1500);
        yield return new CollectibleItem("Levels Required", "LevelsRequired", MakeImgPath(GoalsFolder, "Levels"), 32);
        yield return new ToggleItem("Chaos Emeralds Required", "EmeraldsRequired", MakeImgPath(GoalsFolder, "Emeralds"));
        yield return new CollectibleItem("Bosses Required", "BossesRequired", MakeImgPath(GoalsFolder, "Bosses"), 15);
        yield return new CollectibleItem("Missions Required", "MissionsRequired", MakeImgPath(GoalsFolder, "Missions"), 60);
        yield return new ToggleItem("Chao Races Required", "ChaoRacesRequired", MakeImgPath(GoalsFolder, "ChaoRaces"));
        yield return new CollectibleItem("Levels Required for Chao Races", "ChaoRacesAccessLevels", MakeImgPath(GoalsFolder, "ChaoRacesAccessLevels"), 128);
    }

    private static IEnumerable<Item> GenerateLogicSettings()
    {
        yield return new ProgressiveItem("Logic Level", "LogicLevel", true, false, 0,
        [
            new ProgressiveItemStage("Normal Logic", "NormalLogic", MakeImgPath(LogicFolder, "NormalLogic"), false),
            new ProgressiveItemStage("Hard Logic", "HardLogic", MakeImgPath(LogicFolder, "HardLogic"), false),
            new ProgressiveItemStage("Expert DC Logic", "ExpertLogicDC", MakeImgPath(LogicFolder, "ExpertLogicDC"), false),
            new ProgressiveItemStage("Expert DX Logic", "ExpertLogicDX", MakeImgPath(LogicFolder, "ExpertLogicDX"), false),
            new ProgressiveItemStage("Expert DX+ Logic", "ExpertLogicDXPlus", MakeImgPath(LogicFolder, "ExpertLogicDXPlus"), false)
        ]);

        yield return new ProgressiveItem("Lazy Fishing", "LazyFishing", true, false, 0,
        [
            new ProgressiveItemStage("Lazy Fishing - Off", "LazyFishingOff", MakeImgPath(LogicFolder, "LazyFishingOff"), false),
            new ProgressiveItemStage("Lazy Fishing - No Logic", "LazyFishingNoLogic", MakeImgPath(LogicFolder, "LazyFishingNoLogic"), false),
            new ProgressiveItemStage("Lazy Fishing - Fishsanity", "LazyFishingFishsanity", MakeImgPath(LogicFolder, "LazyFishingFishsanity"), false),
            new ProgressiveItemStage("Lazy Fishing - All", "LazyFishingAll", MakeImgPath(LogicFolder, "LazyFishingAll"), false)
        ]);

        yield return new ToggleItem("Include Field Emblem Checks", "FieldEmblemChecks", MakeImgPath(LogicFolder, "FieldEmblemChecks"));
        yield return new ToggleItem("Include Mission Mode Checks", "MissionModeChecks", MakeImgPath(LogicFolder, "MissionModeChecks"));
        yield return new ToggleItem("Auto-start Missions", "AutoStartMissions", MakeImgPath(LogicFolder, "AutoStartMissions"));
        yield return new ToggleItem("Include Secret Chao Eggs", "SecretChaoEggs", MakeImgPath(LogicFolder, "SecretChaoEggs"));
        yield return new ToggleItem("Include Chao Races", "ChaoRacesChecks", MakeImgPath(LogicFolder, "ChaoRacesChecks"));

        yield return new ToggleItem("Unify Egg Hornet", "UnifyEggHornet", MakeImgPath(LogicFolder, "UnifyEggHornet"));
        yield return new ToggleItem("Unify Chaos 4", "UnifyChaos4", MakeImgPath(LogicFolder, "UnifyChaos4"));
        yield return new ToggleItem("Unify Chaos 6", "UnifyChaos6", MakeImgPath(LogicFolder, "UnifyChaos6"));
    }

    private static IEnumerable<Item> GeneratePlayableCharacterSettings()
        => Characters.Select(_ => new ToggleItem($"{_} Playable",
                                                 $"{_}Playable",
                                                 MakeImgPath(CharactersFolder, $"{_}Playable")));

    private static IEnumerable<Item> GenerateStartingLocationSettings()
        => from character in Characters
           select new ProgressiveItem($"{character.ToPossessive()} Starting Location",
                                      $"{character}Start",
                                      true,
                                      false,
                                      0,
                                      from start in Starts
                                      let code = Common.RemoveWhitespace(start).Replace("-", String.Empty)
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
                                                                      MakeImgPath(ObjectivesFolder, $"{character}Mission{mission}")));

    private static IEnumerable<Item> GenerateSubGameSettings()
    {
        yield return new ProgressiveItem("Sky Chase Checks", "SkyChaseChecks", true, true, 0,
        [
            new ProgressiveItemStage("Sky Chase - Normal Mission Only", "EnableSkyChase", MakeImgPath(ObjectivesFolder, "SkyChaseB")),
            new ProgressiveItemStage("Sky Chase - Both Missions", "EnableSkyChaseHard", MakeImgPath(ObjectivesFolder, "SkyChaseA"))
        ]);

        yield return new ProgressiveItem("Sand Hill Checks", "SandHillChecks", true, true, 0,
        [
            new ProgressiveItemStage("Sand Hill - Normal Mission Only", "EnableSandHill", MakeImgPath(ObjectivesFolder, "SandHillB")),
            new ProgressiveItemStage("Sand Hill - Both Missions", "EnableSandHillHard", MakeImgPath(ObjectivesFolder, "SandHillA"))
        ]);

        yield return new ProgressiveItem("Twinkle Circuit Checks", "TwinkleCircuitChecks", true, true, 0,
        [
            new ProgressiveItemStage("Twinkle Circuit - Single Check", "EnableTwinkleCircuit", MakeImgPath(ObjectivesFolder, "TwinkleCircuit"), false),
            new ProgressiveItemStage("Twinkle Circuit - Character Checks", "EnableTwinkleCircuitMultiple", MakeImgPath(ObjectivesFolder, "TwinkleCircuitMultiple"), false)
        ]);
    }

    private static IEnumerable<Item> GenerateSanitySettings()
    {
        yield return new ToggleItem("Enemysanity", "Enemysanity", MakeImgPath(SanitiesFolder, "Enemysanity"));
        foreach (var i in Characters.Select(_ => new ToggleItem($"{_.ToPossessive()} Enemysanity",
                                                                $"{_}Enemysanity",
                                                                MakeImgPath(SanitiesFolder, $"{_}Enemysanity"))))
            yield return i;

        yield return new ToggleItem("Capsulesanity", "Capsulesanity", MakeImgPath(SanitiesFolder, "Capsulesanity"));
        foreach (var i in from character in Characters
                          from item in Capsules
                          let code = $"{character}{item.Pascalize()}Capsulesanity"
                          select new ToggleItem($"{character.ToPossessive()} {item} Capsulesanity",
                                                code,
                                                MakeImgPath(SanitiesFolder, code)))
            yield return i;

        yield return new ToggleItem("Missable Enemies", "MissableEnemies", MakeImgPath(SanitiesFolder, "MissableEnemies"));
        yield return new ToggleItem("Missable Capsules", "MissableCapsules", MakeImgPath(SanitiesFolder, "MissableCapsules"));
        yield return new ToggleItem("Pinball Capsules", "PinballCapsules", MakeImgPath(SanitiesFolder, "PinballCapsules"));

        yield return new ToggleItem("Fishsanity", "Fishsanity", MakeImgPath(SanitiesFolder, "Fishsanity"));
    }

    private static IEnumerable<Item> GenerateMissionsSettings()
        => Enumerable.Range(1, 60)
                     .Select(_ => new ToggleItem($"Allow Mission {_}",
                                                 $"AllowMission{_}",
                                                 MakeImgPath("goals", "Missions")));
}