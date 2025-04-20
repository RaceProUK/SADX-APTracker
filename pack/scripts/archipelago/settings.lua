function ResetSettings()
    for _, code in pairs(Settings) do
        local item = Tracker:FindObjectForCode(code)
        if item then
            print(item.Type)
            if item.Type == "toggle" then
                item.Active = false
            elseif item.Type == "consumable" then
                item.AcquiredCount = 0
            elseif item.Type == "progressive" then
                item.CurrentStage = 0
            end
        end
    end
end

function ParseSettings(slotData)
    if slotData["LogicLevel"] then
        local setting = Tracker:FindObjectForCode(Settings.LogicLevel)
        setting.CurrentStage = math.tointeger(slotData["LogicLevel"]) or 0
    end
    if slotData["EmblemsForPerfectChaos"] then
        local setting = Tracker:FindObjectForCode(Settings.EmblemsRequired)
        setting.AcquiredCount = math.tointeger(slotData["EmblemsForPerfectChaos"]) or 0
    end
    if slotData["LevelForPerfectChaos"] then
        local setting = Tracker:FindObjectForCode(Settings.LevelsRequired)
        setting.AcquiredCount = math.tointeger(slotData["LevelForPerfectChaos"]) or 0
    end
    if slotData["GoalRequiresChaosEmeralds"] then
        local setting = Tracker:FindObjectForCode(Settings.EmeraldsRequired)
        setting.Active = slotData["GoalRequiresChaosEmeralds"] ~= 0
    end
    if slotData["BossesForPerfectChaos"] then
        local setting = Tracker:FindObjectForCode(Settings.BossesRequired)
        setting.AcquiredCount = math.tointeger(slotData["BossesForPerfectChaos"]) or 0
    end
    if slotData["MissionForPerfectChaos"] then
        local setting = Tracker:FindObjectForCode(Settings.MissionsRequired)
        setting.AcquiredCount = math.tointeger(slotData["MissionForPerfectChaos"]) or 0
    end
    if slotData["GoalRequiresChaoRaces"] then
        local setting = Tracker:FindObjectForCode(Settings.ChaoRacesRequired)
        setting.Active = slotData["GoalRequiresChaoRaces"] ~= 0
    end
    if slotData["FieldEmblemChecks"] then
        local setting = Tracker:FindObjectForCode(Settings.FieldEmblemChecks)
        setting.Active = slotData["FieldEmblemChecks"] ~= 0
    end
    if slotData["SecretChaoEggs"] then
        local setting = Tracker:FindObjectForCode(Settings.SecretChaoEggs)
        setting.Active = slotData["SecretChaoEggs"] ~= 0
    end
    if slotData["UnifyEggHornet"] then
        local setting = Tracker:FindObjectForCode(Settings.UnifyEggHornet)
        setting.Active = slotData["UnifyEggHornet"] ~= 0
    end
    if slotData["UnifyChaos4"] then
        local setting = Tracker:FindObjectForCode(Settings.UnifyChaos4)
        setting.Active = slotData["UnifyChaos4"] ~= 0
    end
    if slotData["UnifyChaos6"] then
        local setting = Tracker:FindObjectForCode(Settings.UnifyChaos6)
        setting.Active = slotData["UnifyChaos6"] ~= 0
    end
    if slotData["ChaoRacesLevelsToAccessPercentage"] then
        local setting = Tracker:FindObjectForCode(Settings.ChaoRacesAccessLevels)
        setting.AcquiredCount = math.tointeger(slotData["ChaoRacesLevelsToAccessPercentage"]) or 0
    end
    if slotData["SkyChaseChecks"] and slotData["SkyChaseChecksHard"] then
        local setting = Tracker:FindObjectForCode(Settings.SkyChaseChecks)
        setting.CurrentStage = (math.tointeger(slotData["SkyChaseChecks"]) + math.tointeger(slotData["SkyChaseChecksHard"])) or 0
    end
    if slotData["SandHillCheck"] and slotData["SandHillCheckHard"] then
        local setting = Tracker:FindObjectForCode(Settings.SandHillChecks)
        setting.CurrentStage = (math.tointeger(slotData["SandHillCheck"]) + math.tointeger(slotData["SandHillCheckHard"])) or 0
    end
    if slotData["TwinkleCircuitCheck"] and slotData["MultipleTwinkleCircuitChecks"] then
        local setting = Tracker:FindObjectForCode(Settings.TwinkleCircuitChecks)
        setting.CurrentStage = (math.tointeger(slotData["TwinkleCircuitCheck"]) + math.tointeger(slotData["MultipleTwinkleCircuitChecks"])) or 0
    end
    if slotData["PlayableSonic"] then
        local setting = Tracker:FindObjectForCode(Settings.SonicPlayable)
        setting.Active = slotData["PlayableSonic"] ~= 0
    end
    if slotData["PlayableTails"] then
        local setting = Tracker:FindObjectForCode(Settings.TailsPlayable)
        setting.Active = slotData["PlayableTails"] ~= 0
    end
    if slotData["PlayableKnuckles"] then
        local setting = Tracker:FindObjectForCode(Settings.KnucklesPlayable)
        setting.Active = slotData["PlayableKnuckles"] ~= 0
    end
    if slotData["PlayableAmy"] then
        local setting = Tracker:FindObjectForCode(Settings.AmyPlayable)
        setting.Active = slotData["PlayableAmy"] ~= 0
    end
    if slotData["PlayableGamma"] then
        local setting = Tracker:FindObjectForCode(Settings.GammaPlayable)
        setting.Active = slotData["PlayableGamma"] ~= 0
    end
    if slotData["PlayableBig"] then
        local setting = Tracker:FindObjectForCode(Settings.BigPlayable)
        setting.Active = slotData["PlayableBig"] ~= 0
    end
    if slotData["SonicActionStageMissions"] then
        local setting = Tracker:FindObjectForCode(Settings.SonicMissions)
        setting.CurrentStage = math.tointeger(slotData["SonicActionStageMissions"]) or 0
    end
    if slotData["TailsActionStageMissions"] then
        local setting = Tracker:FindObjectForCode(Settings.TailsMissions)
        setting.CurrentStage = math.tointeger(slotData["TailsActionStageMissions"]) or 0
    end
    if slotData["KnucklesActionStageMissions"] then
        local setting = Tracker:FindObjectForCode(Settings.KnucklesMissions)
        setting.CurrentStage = math.tointeger(slotData["KnucklesActionStageMissions"]) or 0
    end
    if slotData["AmyActionStageMissions"] then
        local setting = Tracker:FindObjectForCode(Settings.AmyMissions)
        setting.CurrentStage = math.tointeger(slotData["AmyActionStageMissions"]) or 0
    end
    if slotData["GammaActionStageMissions"] then
        local setting = Tracker:FindObjectForCode(Settings.GammaMissions)
        setting.CurrentStage = math.tointeger(slotData["GammaActionStageMissions"]) or 0
    end
    if slotData["BigActionStageMissions"] then
        local setting = Tracker:FindObjectForCode(Settings.BigMissions)
        setting.CurrentStage = math.tointeger(slotData["BigActionStageMissions"]) or 0
    end
    if slotData["EnemySanity"] then
        local setting = Tracker:FindObjectForCode(Settings.Enemysanity)
        setting.Active = slotData["EnemySanity"] ~= 0
    end
    if slotData["SonicEnemySanity"] then
        local setting = Tracker:FindObjectForCode(Settings.SonicEnemysanity)
        setting.Active = slotData["SonicEnemySanity"] ~= 0
    end
    if slotData["TailsEnemySanity"] then
        local setting = Tracker:FindObjectForCode(Settings.TailsEnemysanity)
        setting.Active = slotData["TailsEnemySanity"] ~= 0
    end
    if slotData["KnucklesEnemySanity"] then
        local setting = Tracker:FindObjectForCode(Settings.KnucklesEnemysanity)
        setting.Active = slotData["KnucklesEnemySanity"] ~= 0
    end
    if slotData["AmyEnemySanity"] then
        local setting = Tracker:FindObjectForCode(Settings.AmyEnemysanity)
        setting.Active = slotData["AmyEnemySanity"] ~= 0
    end
    if slotData["GammaEnemySanity"] then
        local setting = Tracker:FindObjectForCode(Settings.GammaEnemysanity)
        setting.Active = slotData["GammaEnemySanity"] ~= 0
    end
    if slotData["BigEnemySanity"] then
        local setting = Tracker:FindObjectForCode(Settings.BigEnemysanity)
        setting.Active = slotData["BigEnemySanity"] ~= 0
    end
    if slotData["CapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.Capsulesanity)
        setting.Active = slotData["CapsuleSanity"] ~= 0
    end
    if slotData["SonicCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.SonicCapsulesanity)
        setting.Active = slotData["SonicCapsuleSanity"] ~= 0
    end
    if slotData["TailsCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.TailsCapsulesanity)
        setting.Active = slotData["TailsCapsuleSanity"] ~= 0
    end
    if slotData["KnucklesCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.KnucklesCapsulesanity)
        setting.Active = slotData["KnucklesCapsuleSanity"] ~= 0
    end
    if slotData["AmyCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.AmyCapsulesanity)
        setting.Active = slotData["AmyCapsuleSanity"] ~= 0
    end
    if slotData["GammaCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.GammaCapsulesanity)
        setting.Active = slotData["GammaCapsuleSanity"] ~= 0
    end
    if slotData["BigCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.BigCapsulesanity)
        setting.Active = slotData["BigCapsuleSanity"] ~= 0
    end
    if slotData["PinballCapsules"] then
        local setting = Tracker:FindObjectForCode(Settings.PinballCapsules)
        setting.Active = slotData["PinballCapsules"] ~= 0
    end
    if slotData["LifeCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.LifeCapsulesanity)
        setting.Active = slotData["LifeCapsuleSanity"] ~= 0
    end
    if slotData["ShieldCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.ShieldCapsulesanity)
        setting.Active = slotData["ShieldCapsuleSanity"] ~= 0
    end
    if slotData["PowerUpCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.PowerupCapsulesanity)
        setting.Active = slotData["PowerUpCapsuleSanity"] ~= 0
    end
    if slotData["RingCapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.RingCapsulesanity)
        setting.Active = slotData["RingCapsuleSanity"] ~= 0
    end
    if slotData["FishSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.Fishsanity)
        setting.Active = slotData["FishSanity"] ~= 0
    end
    if slotData["MissionBlackList"] then
        --TODO: Handle mission blacklist
        for k, v in pairs(slotData["MissionBlackList"]) do
            print("[" .. k .. "] = " .. tostring(v))
        end
    end
end