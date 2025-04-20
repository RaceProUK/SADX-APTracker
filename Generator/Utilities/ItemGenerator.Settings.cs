using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class ItemGenerator
{
    private static async Task GenerateSettingsJson()
    {
        static string MakeImgPath(string img) => $"images/settings/{img}.png";

        var settings = new List<Item>
        {
            new ProgressiveItem("Logic Level", "LogicLevel", true, false, 0,
            [
                new ProgressiveItemStage("Normal Logic", "NormalLogic", MakeImgPath("NormalLogic"), false),
                new ProgressiveItemStage("Hard Logic", "HardLogic", MakeImgPath("HardLogic"), false),
                new ProgressiveItemStage("Expert DC Logic", "ExpertLogicDC", MakeImgPath("ExpertLogicDC"), false),
                new ProgressiveItemStage("Expert DX Logic", "ExpertLogicDX", MakeImgPath("ExpertLogicDX"), false)
            ]),
            new CollectibleItem("Emblems Required", "EmblemsRequired", MakeImgPath("Emblems"), 130),
            new CollectibleItem("Levels Required", "LevelsRequired", MakeImgPath("Levels"), 128),
            new ToggleItem("Chaos Emeralds Required", "EmeraldsRequired", MakeImgPath("Emeralds")),
            new CollectibleItem("Bosses Required", "BossesRequired", MakeImgPath("Bosses"), 18),
            new CollectibleItem("Missions Required", "MissionsRequired", MakeImgPath("Missions"), 60),
            new ToggleItem("Chao Races Required", "ChaoRacesRequired", MakeImgPath("ChaoRaces")),

            new ToggleItem("Include Field Emblem Checks", "FieldEmblemChecks", MakeImgPath("FieldEmblemChecks")),
            new ToggleItem("Secret Chao Eggs", "SecretChaoEggs", MakeImgPath("SecretChaoEggs")),
            new ToggleItem("Unify Egg Hornet", "UnifyEggHornet", MakeImgPath("UnifyEggHornet")),
            new ToggleItem("Unify Chaos 4", "UnifyChaos4", MakeImgPath("UnifyChaos4")),
            new ToggleItem("Unify Chaos 6", "UnifyChaos6", MakeImgPath("UnifyChaos6")),
            new CollectibleItem("Levels Required for Chao Races", "ChaoRacesAccessLevels", MakeImgPath("ChaoRacesAccessLevels"), 128),

            new ProgressiveItem("Sky Chase Checks", "SkyChaseChecks", true, true, 0,
            [
                new ProgressiveItemStage("Sky Chase - Normal Mission Only", "EnableSkyChase", MakeImgPath("SkyChaseB")),
                new ProgressiveItemStage("Sky Chase - Both Missions", "EnableSkyChaseHard", MakeImgPath("SkyChaseA"))
            ]),
            new ProgressiveItem("Sand Hill Checks", "SandHillChecks", true, true, 0,
            [
                new ProgressiveItemStage("Sand Hill - Normal Mission Only", "EnableSandHill", MakeImgPath("SandHillB")),
                new ProgressiveItemStage("Sand Hill - Both Missions", "EnableSandHillHard", MakeImgPath("SandHillA"))
            ]),
            new ProgressiveItem("Twinkle Circuit Checks", "TwinkleCircuitChecks", true, true, 0,
            [
                new ProgressiveItemStage("Twinkle Circuit - Single Check", "EnableTwinkleCircuit", MakeImgPath("TwinkleCircuit"), false),
                new ProgressiveItemStage("Twinkle Circuit - Character Checks", "EnableTwinkleCircuitMultiple", MakeImgPath("TwinkleCircuitMultiple"), false)
            ]),

            new ToggleItem("Sonic Playable", "SonicPlayable", MakeImgPath("SonicPlayable")),
            new ToggleItem("Tails Playable", "TailsPlayable", MakeImgPath("TailsPlayable")),
            new ToggleItem("Knuckles Playable", "KnucklesPlayable", MakeImgPath("KnucklesPlayable")),
            new ToggleItem("Amy Playable", "AmyPlayable", MakeImgPath("AmyPlayable")),
            new ToggleItem("Gamma Playable", "GammaPlayable", MakeImgPath("GammaPlayable")),
            new ToggleItem("Big Playable", "BigPlayable", MakeImgPath("BigPlayable")),

            new ProgressiveItem("Sonic Missions", "SonicMissions", true, true, 0,
            [
                new ProgressiveItemStage("Sonic's C Missions", "SonicMissionC", MakeImgPath("SonicMissionC")),
                new ProgressiveItemStage("Sonic's B Missions", "SonicMissionB", MakeImgPath("SonicMissionB")),
                new ProgressiveItemStage("Sonic's A Missions", "SonicMissionA", MakeImgPath("SonicMissionA")),
                new ProgressiveItemStage("Sonic's S Missions", "SonicMissionS", MakeImgPath("SonicMissionS"))
            ]),
            new ProgressiveItem("Tails Missions", "TailsMissions", true, true, 0,
            [
                new ProgressiveItemStage("Tails' C Missions", "TailsMissionC", MakeImgPath("TailsMissionC")),
                new ProgressiveItemStage("Tails' B Missions", "TailsMissionB", MakeImgPath("TailsMissionB")),
                new ProgressiveItemStage("Tails' A Missions", "TailsMissionA", MakeImgPath("TailsMissionA")),
                new ProgressiveItemStage("Tails' S Missions", "TailsMissionS", MakeImgPath("TailsMissionS"))
            ]),
            new ProgressiveItem("Knuckles Missions", "KnucklesMissions", true, true, 0,
            [
                new ProgressiveItemStage("Knuckles' C Missions", "KnucklesMissionC", MakeImgPath("KnucklesMissionC")),
                new ProgressiveItemStage("Knuckles' B Missions", "KnucklesMissionB", MakeImgPath("KnucklesMissionB")),
                new ProgressiveItemStage("Knuckles' A Missions", "KnucklesMissionA", MakeImgPath("KnucklesMissionA")),
                new ProgressiveItemStage("Knuckles' S Missions", "KnucklesMissionS", MakeImgPath("KnucklesMissionS"))
            ]),
            new ProgressiveItem("Amy Missions", "AmyMissions", true, true, 0,
            [
                new ProgressiveItemStage("Amy's C Missions", "AmyMissionC", MakeImgPath("AmyMissionC")),
                new ProgressiveItemStage("Amy's B Missions", "AmyMissionB", MakeImgPath("AmyMissionB")),
                new ProgressiveItemStage("Amy's A Missions", "AmyMissionA", MakeImgPath("AmyMissionA")),
                new ProgressiveItemStage("Amy's S Missions", "AmyMissionS", MakeImgPath("AmyMissionS"))
            ]),
            new ProgressiveItem("Gamma Missions", "GammaMissions", true, true, 0,
            [
                new ProgressiveItemStage("Gamma's C Missions", "GammaMissionC", MakeImgPath("GammaMissionC")),
                new ProgressiveItemStage("Gamma's B Missions", "GammaMissionB", MakeImgPath("GammaMissionB")),
                new ProgressiveItemStage("Gamma's A Missions", "GammaMissionA", MakeImgPath("GammaMissionA")),
                new ProgressiveItemStage("Gamma's S Missions", "GammaMissionS", MakeImgPath("GammaMissionS"))
            ]),
            new ProgressiveItem("Big Missions", "BigMissions", true, true, 0,
            [
                new ProgressiveItemStage("Big's C Missions", "BigMissionC", MakeImgPath("BigMissionC")),
                new ProgressiveItemStage("Big's B Missions", "BigMissionB", MakeImgPath("BigMissionB")),
                new ProgressiveItemStage("Big's A Missions", "BigMissionA", MakeImgPath("BigMissionA")),
                new ProgressiveItemStage("Big's S Missions", "BigMissionS", MakeImgPath("BigMissionS"))
            ]),

            new ToggleItem("Enemysanity", "Enemysanity", MakeImgPath("Enemysanity")),
            new ToggleItem("Sonic's Enemysanity", "SonicEnemysanity", MakeImgPath("SonicEnemysanity")),
            new ToggleItem("Tails's Enemysanity", "TailsEnemysanity", MakeImgPath("TailsEnemysanity")),
            new ToggleItem("Knuckles's Enemysanity", "KnucklesEnemysanity", MakeImgPath("KnucklesEnemysanity")),
            new ToggleItem("Amy's Enemysanity", "AmyEnemysanity", MakeImgPath("AmyEnemysanity")),
            new ToggleItem("Gamma's Enemysanity", "GammaEnemysanity", MakeImgPath("GammaEnemysanity")),
            new ToggleItem("Big's Enemysanity", "BigEnemysanity", MakeImgPath("BigEnemysanity")),

            new ToggleItem("Capsulesanity", "Capsulesanity", MakeImgPath("Capsulesanity")),
            new ToggleItem("Sonic's Capsulesanity", "SonicCapsulesanity", MakeImgPath("SonicCapsulesanity")),
            new ToggleItem("Tails's Capsulesanity", "TailsCapsulesanity", MakeImgPath("TailsCapsulesanity")),
            new ToggleItem("Knuckles's Capsulesanity", "KnucklesCapsulesanity", MakeImgPath("KnucklesCapsulesanity")),
            new ToggleItem("Amy's Capsulesanity", "AmyCapsulesanity", MakeImgPath("AmyCapsulesanity")),
            new ToggleItem("Gamma's Capsulesanity", "GammaCapsulesanity", MakeImgPath("GammaCapsulesanity")),
            new ToggleItem("Big's Capsulesanity", "BigCapsulesanity", MakeImgPath("BigCapsulesanity")),

            new ToggleItem("Pinball Capsules", "PinballCapsules", MakeImgPath("PinballCapsules")),
            new ToggleItem("Life Capsulesanity", "LifeCapsulesanity", MakeImgPath("LifeCapsulesanity")),
            new ToggleItem("Shield Capsulesanity", "ShieldCapsulesanity", MakeImgPath("ShieldCapsulesanity")),
            new ToggleItem("Power-up Capsulesanity", "PowerupCapsulesanity", MakeImgPath("PowerupCapsulesanity")),
            new ToggleItem("Ring Capsulesanity", "RingCapsulesanity", MakeImgPath("RingCapsulesanity")),
            new ToggleItem("Fishsanity", "Fishsanity", MakeImgPath("Fishsanity")),
        };
        await FileWriter.WriteFile(JsonSerializer.Serialize(settings, Constants.JsonOptions),
                                   "settings.json",
                                   "items");
    }
}