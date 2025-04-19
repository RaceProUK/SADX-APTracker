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
                new ProgressiveItemStage("Normal Logic", "NormalLogic", MakeImgPath("NormalLogic")),
                new ProgressiveItemStage("Hard Logic", "HardLogic", MakeImgPath("HardLogic")),
                new ProgressiveItemStage("Expert DC Logic", "ExpertLogicDC", MakeImgPath("ExpertLogicDC")),
                new ProgressiveItemStage("Expert DX Logic", "ExpertLogicDX", MakeImgPath("ExpertLogicDX"))
            ]),
            new CollectibleItem("Emblems Required", "EmblemsRequired", MakeImgPath("Emblems"), 130),
            new CollectibleItem("Levels Required", "LevelsRequired", MakeImgPath("Levels"), 128),
            new ToggleItem("Chaos Emeralds Required", "EmeraldsRequired", MakeImgPath("Emeralds")),
            new CollectibleItem("Bosses Required", "BossesRequired", MakeImgPath("Bosses"), 18),
            new CollectibleItem("Missions Required", "MissionsRequired", MakeImgPath("Missions"), 60),
            new ToggleItem("Chao Races Required", "ChaoRacesRequired", MakeImgPath("ChaoRaces")),

            new ToggleItem("Secret Chao Eggs", "SecretChaoEggs", MakeImgPath("SecretChaoEggs")),
            new ToggleItem("Include Field Emblem Checks", "FieldEmblemChecks", MakeImgPath("FieldEmblemChecks")),
            new ToggleItem("Unify Twinkle Circuit", "UnifyTwinkleCircuit", MakeImgPath("UnifyTwinkleCircuit")),
            new ToggleItem("Unify Egg Hornet", "UnifyEggHornet", MakeImgPath("UnifyEggHornet")),
            new ToggleItem("Unify Chaos 4", "UnifyChaos4", MakeImgPath("UnifyChaos4")),
            new ToggleItem("Unify Chaos 6", "UnifyChaos6", MakeImgPath("UnifyChaos6")),
            new CollectibleItem("Levels Required for Chao Races", "ChaoRacesAccessLevels", MakeImgPath("ChaoRacesAccessLevels"), 128),

            new ToggleItem("Sonic Playable", "SonicPlayable", MakeImgPath("SonicPlayable")),
            new ToggleItem("Tails Playable", "TailsPlayable", MakeImgPath("TailsPlayable")),
            new ToggleItem("Knuckles Playable", "KnucklesPlayable", MakeImgPath("KnucklesPlayable")),
            new ToggleItem("Amy Playable", "AmyPlayable", MakeImgPath("AmyPlayable")),
            new ToggleItem("Gamma Playable", "GammaPlayable", MakeImgPath("GammaPlayable")),
            new ToggleItem("Big Playable", "BigPlayable", MakeImgPath("BigPlayable")),

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
        };
        await FileWriter.WriteFile(JsonSerializer.Serialize(settings, Constants.JsonOptions),
                                   "settings.json",
                                   "items");
    }
}