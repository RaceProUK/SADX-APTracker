using System.Text.Json;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static partial class ItemGenerator
{
    private static async Task GenerateSettingsJson()
    {
        static string MakeImgPath(string section, string img) => $"images/settings/{section}/{img}.png";

        var settings = new List<Item>
        {
            new CollectibleItem("Emblems Required", "EmblemsRequired", MakeImgPath("goals", "Emblems"), 1500),
            new CollectibleItem("Levels Required", "LevelsRequired", MakeImgPath("goals", "Levels"), 32),
            new ToggleItem("Chaos Emeralds Required", "EmeraldsRequired", MakeImgPath("goals", "Emeralds")),
            new CollectibleItem("Bosses Required", "BossesRequired", MakeImgPath("goals", "Bosses"), 15),
            new CollectibleItem("Missions Required", "MissionsRequired", MakeImgPath("goals", "Missions"), 60),
            new ToggleItem("Chao Races Required", "ChaoRacesRequired", MakeImgPath("goals", "ChaoRaces")),
            new CollectibleItem("Levels Required for Chao Races", "ChaoRacesAccessLevels", MakeImgPath("goals", "ChaoRacesAccessLevels"), 128),

            new ProgressiveItem("Logic Level", "LogicLevel", true, false, 0,
            [
                new ProgressiveItemStage("Normal Logic", "NormalLogic", MakeImgPath("logic", "NormalLogic"), false),
                new ProgressiveItemStage("Hard Logic", "HardLogic", MakeImgPath("logic", "HardLogic"), false),
                new ProgressiveItemStage("Expert DC Logic", "ExpertLogicDC", MakeImgPath("logic", "ExpertLogicDC"), false),
                new ProgressiveItemStage("Expert DX Logic", "ExpertLogicDX", MakeImgPath("logic", "ExpertLogicDX"), false)
            ]),
            new ToggleItem("Include Field Emblem Checks", "FieldEmblemChecks", MakeImgPath("logic", "FieldEmblemChecks")),
            new ToggleItem("Secret Chao Eggs", "SecretChaoEggs", MakeImgPath("logic", "SecretChaoEggs")),
            new ToggleItem("Unify Egg Hornet", "UnifyEggHornet", MakeImgPath("logic", "UnifyEggHornet")),
            new ToggleItem("Unify Chaos 4", "UnifyChaos4", MakeImgPath("logic", "UnifyChaos4")),
            new ToggleItem("Unify Chaos 6", "UnifyChaos6", MakeImgPath("logic", "UnifyChaos6")),

            new ToggleItem("Sonic Playable", "SonicPlayable", MakeImgPath("characters", "SonicPlayable")),
            new ToggleItem("Tails Playable", "TailsPlayable", MakeImgPath("characters", "TailsPlayable")),
            new ToggleItem("Knuckles Playable", "KnucklesPlayable", MakeImgPath("characters", "KnucklesPlayable")),
            new ToggleItem("Amy Playable", "AmyPlayable", MakeImgPath("characters", "AmyPlayable")),
            new ToggleItem("Gamma Playable", "GammaPlayable", MakeImgPath("characters", "GammaPlayable")),
            new ToggleItem("Big Playable", "BigPlayable", MakeImgPath("characters", "BigPlayable")),

            new ProgressiveItem("Sonic's Starting Location", "SonicStart", true, false, 0,
            [
                new ProgressiveItemStage("Station Square (Hub)", "SonicStationSquareMain", MakeImgPath("starts", "SonicStationSquareMain"), false),
                new ProgressiveItemStage("Station", "SonicStation", MakeImgPath("starts", "SonicStation"), false),
                new ProgressiveItemStage("Hotel", "SonicHotel", MakeImgPath("starts", "SonicHotel"), false),
                new ProgressiveItemStage("Casino", "SonicCasino", MakeImgPath("starts", "SonicCasino"), false),
                new ProgressiveItemStage("Twinkle Park Lobby", "SonicTwinkleParkLobby", MakeImgPath("starts", "SonicTwinkleParkLobby"), false),
                new ProgressiveItemStage("Mystic Ruins (Hub)", "SonicMysticRuinsMain", MakeImgPath("starts", "SonicMysticRuinsMain"), false),
                new ProgressiveItemStage("Angel Island", "SonicAngelIsland", MakeImgPath("starts", "SonicAngelIsland"), false),
                new ProgressiveItemStage("Jungle", "SonicJungle", MakeImgPath("starts", "SonicJungle"), false),
                new ProgressiveItemStage("Egg Carrier Outside", "SonicEggCarrierOutside", MakeImgPath("starts", "SonicEggCarrierOutside"), false),
                new ProgressiveItemStage("Egg Carrier Inside", "SonicEggCarrierInside", MakeImgPath("starts", "SonicEggCarrierInside"), false),
                new ProgressiveItemStage("Egg Carrier Front Deck", "SonicEggCarrierFrontDeck", MakeImgPath("starts", "SonicEggCarrierFrontDeck"), false)
            ]),
            new ProgressiveItem("Tails Starting Location", "TailsStart", true, false, 0,
            [
                new ProgressiveItemStage("Station Square (Hub)", "TailsStationSquareMain", MakeImgPath("starts", "TailsStationSquareMain"), false),
                new ProgressiveItemStage("Station", "TailsStation", MakeImgPath("starts", "TailsStation"), false),
                new ProgressiveItemStage("Hotel", "TailsHotel", MakeImgPath("starts", "TailsHotel"), false),
                new ProgressiveItemStage("Casino", "TailsCasino", MakeImgPath("starts", "TailsCasino"), false),
                new ProgressiveItemStage("Twinkle Park Lobby", "TailsTwinkleParkLobby", MakeImgPath("starts", "TailsTwinkleParkLobby"), false),
                new ProgressiveItemStage("Mystic Ruins (Hub)", "TailsMysticRuinsMain", MakeImgPath("starts", "TailsMysticRuinsMain"), false),
                new ProgressiveItemStage("Angel Island", "TailsAngelIsland", MakeImgPath("starts", "TailsAngelIsland"), false),
                new ProgressiveItemStage("Jungle", "TailsJungle", MakeImgPath("starts", "TailsJungle"), false),
                new ProgressiveItemStage("Egg Carrier Outside", "TailsEggCarrierOutside", MakeImgPath("starts", "TailsEggCarrierOutside"), false),
                new ProgressiveItemStage("Egg Carrier Inside", "TailsEggCarrierInside", MakeImgPath("starts", "TailsEggCarrierInside"), false),
                new ProgressiveItemStage("Egg Carrier Front Deck", "TailsEggCarrierFrontDeck", MakeImgPath("starts", "TailsEggCarrierFrontDeck"), false)
            ]),
            new ProgressiveItem("Knuckles' Starting Location", "KnucklesStart", true, false, 0,
            [
                new ProgressiveItemStage("Station Square (Hub)", "KnucklesStationSquareMain", MakeImgPath("starts", "KnucklesStationSquareMain"), false),
                new ProgressiveItemStage("Station", "KnucklesStation", MakeImgPath("starts", "KnucklesStation"), false),
                new ProgressiveItemStage("Hotel", "KnucklesHotel", MakeImgPath("starts", "KnucklesHotel"), false),
                new ProgressiveItemStage("Casino", "KnucklesCasino", MakeImgPath("starts", "KnucklesCasino"), false),
                new ProgressiveItemStage("Twinkle Park Lobby", "KnucklesTwinkleParkLobby", MakeImgPath("starts", "KnucklesTwinkleParkLobby"), false),
                new ProgressiveItemStage("Mystic Ruins (Hub)", "KnucklesMysticRuinsMain", MakeImgPath("starts", "KnucklesMysticRuinsMain"), false),
                new ProgressiveItemStage("Angel Island", "KnucklesAngelIsland", MakeImgPath("starts", "KnucklesAngelIsland"), false),
                new ProgressiveItemStage("Jungle", "KnucklesJungle", MakeImgPath("starts", "KnucklesJungle"), false),
                new ProgressiveItemStage("Egg Carrier Outside", "KnucklesEggCarrierOutside", MakeImgPath("starts", "KnucklesEggCarrierOutside"), false),
                new ProgressiveItemStage("Egg Carrier Inside", "KnucklesEggCarrierInside", MakeImgPath("starts", "KnucklesEggCarrierInside"), false),
                new ProgressiveItemStage("Egg Carrier Front Deck", "KnucklesEggCarrierFrontDeck", MakeImgPath("starts", "KnucklesEggCarrierFrontDeck"), false)
            ]),
            new ProgressiveItem("Amy's Starting Location", "AmyStart", true, false, 0,
            [
                new ProgressiveItemStage("Station Square (Hub)", "AmyStationSquareMain", MakeImgPath("starts", "AmyStationSquareMain"), false),
                new ProgressiveItemStage("Station", "AmyStation", MakeImgPath("starts", "AmyStation"), false),
                new ProgressiveItemStage("Hotel", "AmyHotel", MakeImgPath("starts", "AmyHotel"), false),
                new ProgressiveItemStage("Casino", "AmyCasino", MakeImgPath("starts", "AmyCasino"), false),
                new ProgressiveItemStage("Twinkle Park Lobby", "AmyTwinkleParkLobby", MakeImgPath("starts", "AmyTwinkleParkLobby"), false),
                new ProgressiveItemStage("Mystic Ruins (Hub)", "AmyMysticRuinsMain", MakeImgPath("starts", "AmyMysticRuinsMain"), false),
                new ProgressiveItemStage("Angel Island", "AmyAngelIsland", MakeImgPath("starts", "AmyAngelIsland"), false),
                new ProgressiveItemStage("Jungle", "AmyJungle", MakeImgPath("starts", "AmyJungle"), false),
                new ProgressiveItemStage("Egg Carrier Outside", "AmyEggCarrierOutside", MakeImgPath("starts", "AmyEggCarrierOutside"), false),
                new ProgressiveItemStage("Egg Carrier Inside", "AmyEggCarrierInside", MakeImgPath("starts", "AmyEggCarrierInside"), false),
                new ProgressiveItemStage("Egg Carrier Front Deck", "AmyEggCarrierFrontDeck", MakeImgPath("starts", "AmyEggCarrierFrontDeck"), false)
            ]),
            new ProgressiveItem("Gamma's Starting Location", "GammaStart", true, false, 0,
            [
                new ProgressiveItemStage("Station Square (Hub)", "GammaStationSquareMain", MakeImgPath("starts", "GammaStationSquareMain"), false),
                new ProgressiveItemStage("Station", "GammaStation", MakeImgPath("starts", "GammaStation"), false),
                new ProgressiveItemStage("Hotel", "GammaHotel", MakeImgPath("starts", "GammaHotel"), false),
                new ProgressiveItemStage("Casino", "GammaCasino", MakeImgPath("starts", "GammaCasino"), false),
                new ProgressiveItemStage("Twinkle Park Lobby", "GammaTwinkleParkLobby", MakeImgPath("starts", "GammaTwinkleParkLobby"), false),
                new ProgressiveItemStage("Mystic Ruins (Hub)", "GammaMysticRuinsMain", MakeImgPath("starts", "GammaMysticRuinsMain"), false),
                new ProgressiveItemStage("Angel Island", "GammaAngelIsland", MakeImgPath("starts", "GammaAngelIsland"), false),
                new ProgressiveItemStage("Jungle", "GammaJungle", MakeImgPath("starts", "GammaJungle"), false),
                new ProgressiveItemStage("Egg Carrier Outside", "GammaEggCarrierOutside", MakeImgPath("starts", "GammaEggCarrierOutside"), false),
                new ProgressiveItemStage("Egg Carrier Inside", "GammaEggCarrierInside", MakeImgPath("starts", "GammaEggCarrierInside"), false),
                new ProgressiveItemStage("Egg Carrier Front Deck", "GammaEggCarrierFrontDeck", MakeImgPath("starts", "GammaEggCarrierFrontDeck"), false)
            ]),
            new ProgressiveItem("Big's Starting Location", "BigStart", true, false, 0,
            [
                new ProgressiveItemStage("Station Square (Hub)", "BigStationSquareMain", MakeImgPath("starts", "BigStationSquareMain"), false),
                new ProgressiveItemStage("Station", "BigStation", MakeImgPath("starts", "BigStation"), false),
                new ProgressiveItemStage("Hotel", "BigHotel", MakeImgPath("starts", "BigHotel"), false),
                new ProgressiveItemStage("Casino", "BigCasino", MakeImgPath("starts", "BigCasino"), false),
                new ProgressiveItemStage("Twinkle Park Lobby", "BigTwinkleParkLobby", MakeImgPath("starts", "BigTwinkleParkLobby"), false),
                new ProgressiveItemStage("Mystic Ruins (Hub)", "BigMysticRuinsMain", MakeImgPath("starts", "BigMysticRuinsMain"), false),
                new ProgressiveItemStage("Angel Island", "BigAngelIsland", MakeImgPath("starts", "BigAngelIsland"), false),
                new ProgressiveItemStage("Jungle", "BigJungle", MakeImgPath("starts", "BigJungle"), false),
                new ProgressiveItemStage("Egg Carrier Outside", "BigEggCarrierOutside", MakeImgPath("starts", "BigEggCarrierOutside"), false),
                new ProgressiveItemStage("Egg Carrier Inside", "BigEggCarrierInside", MakeImgPath("starts", "BigEggCarrierInside"), false),
                new ProgressiveItemStage("Egg Carrier Front Deck", "BigEggCarrierFrontDeck", MakeImgPath("starts", "BigEggCarrierFrontDeck"), false)
            ]),

            new ProgressiveItem("Sonic Missions", "SonicMissions", true, true, 0,
            [
                new ProgressiveItemStage("Sonic's C Missions", "SonicMissionC", MakeImgPath("objectives", "SonicMissionC")),
                new ProgressiveItemStage("Sonic's B Missions", "SonicMissionB", MakeImgPath("objectives", "SonicMissionB")),
                new ProgressiveItemStage("Sonic's A Missions", "SonicMissionA", MakeImgPath("objectives", "SonicMissionA")),
                new ProgressiveItemStage("Sonic's S Missions", "SonicMissionS", MakeImgPath("objectives", "SonicMissionS"))
            ]),
            new ProgressiveItem("Tails Missions", "TailsMissions", true, true, 0,
            [
                new ProgressiveItemStage("Tails' C Missions", "TailsMissionC", MakeImgPath("objectives", "TailsMissionC")),
                new ProgressiveItemStage("Tails' B Missions", "TailsMissionB", MakeImgPath("objectives", "TailsMissionB")),
                new ProgressiveItemStage("Tails' A Missions", "TailsMissionA", MakeImgPath("objectives", "TailsMissionA")),
                new ProgressiveItemStage("Tails' S Missions", "TailsMissionS", MakeImgPath("objectives", "TailsMissionS"))
            ]),
            new ProgressiveItem("Knuckles Missions", "KnucklesMissions", true, true, 0,
            [
                new ProgressiveItemStage("Knuckles' C Missions", "KnucklesMissionC", MakeImgPath("objectives", "KnucklesMissionC")),
                new ProgressiveItemStage("Knuckles' B Missions", "KnucklesMissionB", MakeImgPath("objectives", "KnucklesMissionB")),
                new ProgressiveItemStage("Knuckles' A Missions", "KnucklesMissionA", MakeImgPath("objectives", "KnucklesMissionA")),
                new ProgressiveItemStage("Knuckles' S Missions", "KnucklesMissionS", MakeImgPath("objectives", "KnucklesMissionS"))
            ]),
            new ProgressiveItem("Amy Missions", "AmyMissions", true, true, 0,
            [
                new ProgressiveItemStage("Amy's C Missions", "AmyMissionC", MakeImgPath("objectives", "AmyMissionC")),
                new ProgressiveItemStage("Amy's B Missions", "AmyMissionB", MakeImgPath("objectives", "AmyMissionB")),
                new ProgressiveItemStage("Amy's A Missions", "AmyMissionA", MakeImgPath("objectives", "AmyMissionA")),
                new ProgressiveItemStage("Amy's S Missions", "AmyMissionS", MakeImgPath("objectives", "AmyMissionS"))
            ]),
            new ProgressiveItem("Gamma Missions", "GammaMissions", true, true, 0,
            [
                new ProgressiveItemStage("Gamma's C Missions", "GammaMissionC", MakeImgPath("objectives", "GammaMissionC")),
                new ProgressiveItemStage("Gamma's B Missions", "GammaMissionB", MakeImgPath("objectives", "GammaMissionB")),
                new ProgressiveItemStage("Gamma's A Missions", "GammaMissionA", MakeImgPath("objectives", "GammaMissionA")),
                new ProgressiveItemStage("Gamma's S Missions", "GammaMissionS", MakeImgPath("objectives", "GammaMissionS"))
            ]),
            new ProgressiveItem("Big Missions", "BigMissions", true, true, 0,
            [
                new ProgressiveItemStage("Big's C Missions", "BigMissionC", MakeImgPath("objectives", "BigMissionC")),
                new ProgressiveItemStage("Big's B Missions", "BigMissionB", MakeImgPath("objectives", "BigMissionB")),
                new ProgressiveItemStage("Big's A Missions", "BigMissionA", MakeImgPath("objectives", "BigMissionA")),
                new ProgressiveItemStage("Big's S Missions", "BigMissionS", MakeImgPath("objectives", "BigMissionS"))
            ]),

            new ProgressiveItem("Sky Chase Checks", "SkyChaseChecks", true, true, 0,
            [
                new ProgressiveItemStage("Sky Chase - Normal Mission Only", "EnableSkyChase", MakeImgPath("objectives", "SkyChaseB")),
                new ProgressiveItemStage("Sky Chase - Both Missions", "EnableSkyChaseHard", MakeImgPath("objectives", "SkyChaseA"))
            ]),
            new ProgressiveItem("Sand Hill Checks", "SandHillChecks", true, true, 0,
            [
                new ProgressiveItemStage("Sand Hill - Normal Mission Only", "EnableSandHill", MakeImgPath("objectives", "SandHillB")),
                new ProgressiveItemStage("Sand Hill - Both Missions", "EnableSandHillHard", MakeImgPath("objectives", "SandHillA"))
            ]),
            new ProgressiveItem("Twinkle Circuit Checks", "TwinkleCircuitChecks", true, true, 0,
            [
                new ProgressiveItemStage("Twinkle Circuit - Single Check", "EnableTwinkleCircuit", MakeImgPath("objectives", "TwinkleCircuit"), false),
                new ProgressiveItemStage("Twinkle Circuit - Character Checks", "EnableTwinkleCircuitMultiple", MakeImgPath("objectives", "TwinkleCircuitMultiple"), false)
            ]),

            new ToggleItem("Enemysanity", "Enemysanity", MakeImgPath("sanities", "Enemysanity")),
            new ToggleItem("Sonic's Enemysanity", "SonicEnemysanity", MakeImgPath("sanities", "SonicEnemysanity")),
            new ToggleItem("Tails' Enemysanity", "TailsEnemysanity", MakeImgPath("sanities", "TailsEnemysanity")),
            new ToggleItem("Knuckles' Enemysanity", "KnucklesEnemysanity", MakeImgPath("sanities", "KnucklesEnemysanity")),
            new ToggleItem("Amy's Enemysanity", "AmyEnemysanity", MakeImgPath("sanities", "AmyEnemysanity")),
            new ToggleItem("Gamma's Enemysanity", "GammaEnemysanity", MakeImgPath("sanities", "GammaEnemysanity")),
            new ToggleItem("Big's Enemysanity", "BigEnemysanity", MakeImgPath("sanities", "BigEnemysanity")),

            new ToggleItem("Capsulesanity", "Capsulesanity", MakeImgPath("sanities", "Capsulesanity")),
            new ToggleItem("Sonic's Capsulesanity", "SonicCapsulesanity", MakeImgPath("sanities", "SonicCapsulesanity")),
            new ToggleItem("Tails' Capsulesanity", "TailsCapsulesanity", MakeImgPath("sanities", "TailsCapsulesanity")),
            new ToggleItem("Knuckles' Capsulesanity", "KnucklesCapsulesanity", MakeImgPath("sanities", "KnucklesCapsulesanity")),
            new ToggleItem("Amy's Capsulesanity", "AmyCapsulesanity", MakeImgPath("sanities", "AmyCapsulesanity")),
            new ToggleItem("Gamma's Capsulesanity", "GammaCapsulesanity", MakeImgPath("sanities", "GammaCapsulesanity")),
            new ToggleItem("Big's Capsulesanity", "BigCapsulesanity", MakeImgPath("sanities", "BigCapsulesanity")),

            new ToggleItem("Pinball Capsules", "PinballCapsules", MakeImgPath("sanities", "PinballCapsules")),
            new ToggleItem("Life Capsulesanity", "LifeCapsulesanity", MakeImgPath("sanities", "LifeCapsulesanity")),
            new ToggleItem("Shield Capsulesanity", "ShieldCapsulesanity", MakeImgPath("sanities", "ShieldCapsulesanity")),
            new ToggleItem("Power-up Capsulesanity", "PowerupCapsulesanity", MakeImgPath("sanities", "PowerupCapsulesanity")),
            new ToggleItem("Ring Capsulesanity", "RingCapsulesanity", MakeImgPath("sanities", "RingCapsulesanity")),
            new ToggleItem("Fishsanity", "Fishsanity", MakeImgPath("sanities", "Fishsanity")),
        };
        for (var i = 1; i <= 60; i++)
            settings.Add(new ToggleItem($"Allow Mission {i}", $"AllowMission{i}", MakeImgPath("goals", "Missions")));
        await FileWriter.WriteFile(JsonSerializer.Serialize(settings, Constants.JsonOptions),
                                   "settings.json",
                                   "items");
    }
}