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
    for i = 1, 60, 1 do
        local code = "AllowMission" .. i
        local setting = Tracker:FindObjectForCode(code)
        if setting then
            setting.Active = true
        end
    end
end

function ParseSettings(slotData)
    if slotData["EmblemsForPerfectChaos"] then
        local setting = Tracker:FindObjectForCode("EmblemsRequired")
        setting.AcquiredCount = math.tointeger(slotData["EmblemsForPerfectChaos"]) or 0
    end
    if slotData["LevelForPerfectChaos"] then
        local setting = Tracker:FindObjectForCode("LevelsRequired")
        setting.AcquiredCount = math.tointeger(slotData["LevelForPerfectChaos"]) or 0
    end
    if slotData["GoalRequiresChaosEmeralds"] then
        local setting = Tracker:FindObjectForCode("EmeraldsRequired")
        setting.Active = slotData["GoalRequiresChaosEmeralds"] ~= 0
    end
    if slotData["BossesForPerfectChaos"] then
        local setting = Tracker:FindObjectForCode("BossesRequired")
        setting.AcquiredCount = math.tointeger(slotData["BossesForPerfectChaos"]) or 0
    end
    if slotData["MissionForPerfectChaos"] then
        local setting = Tracker:FindObjectForCode("MissionsRequired")
        setting.AcquiredCount = math.tointeger(slotData["MissionForPerfectChaos"]) or 0
    end
    if slotData["GoalRequiresChaoRaces"] then
        local setting = Tracker:FindObjectForCode("ChaoRacesRequired")
        setting.Active = slotData["GoalRequiresChaoRaces"] ~= 0
    end
    if slotData["ChaoRacesLevelsToAccessPercentage"] then
        local setting = Tracker:FindObjectForCode("ChaoRacesAccessLevels")
        setting.AcquiredCount = math.tointeger(slotData["ChaoRacesLevelsToAccessPercentage"]) or 0
    end

    if slotData["LogicLevel"] then
        local setting = Tracker:FindObjectForCode("LogicLevel")
        setting.CurrentStage = math.tointeger(slotData["LogicLevel"]) or 0
    end
    if slotData["LazyFishing"] then
        local setting = Tracker:FindObjectForCode("LazyFishing")
        setting.CurrentStage = math.tointeger(slotData["LazyFishing"]) or 0
    end
    if slotData["FieldEmblemChecks"] then
        local setting = Tracker:FindObjectForCode("FieldEmblemChecks")
        setting.Active = slotData["FieldEmblemChecks"] ~= 0
    end
    if slotData["MissionModeChecks"] then
        local setting = Tracker:FindObjectForCode("MissionModeChecks")
        setting.Active = slotData["MissionModeChecks"] ~= 0
    end
    if slotData["AutoStartMissions"] then
        local setting = Tracker:FindObjectForCode("AutoStartMissions")
        setting.Active = slotData["AutoStartMissions"] ~= 0
    end
    if slotData["SecretChaoEggs"] then
        local setting = Tracker:FindObjectForCode("SecretChaoEggs")
        setting.Active = slotData["SecretChaoEggs"] ~= 0
    end
    if slotData["ChaoRacesChecks"] then
        local setting = Tracker:FindObjectForCode("ChaoRacesChecks")
        setting.Active = slotData["ChaoRacesChecks"] ~= 0
    end
    
    if slotData["UnifyEggHornet"] then
        local setting = Tracker:FindObjectForCode("UnifyEggHornet")
        setting.Active = slotData["UnifyEggHornet"] ~= 0
    end
    if slotData["UnifyChaos4"] then
        local setting = Tracker:FindObjectForCode("UnifyChaos4")
        setting.Active = slotData["UnifyChaos4"] ~= 0
    end
    if slotData["UnifyChaos6"] then
        local setting = Tracker:FindObjectForCode("UnifyChaos6")
        setting.Active = slotData["UnifyChaos6"] ~= 0
    end
    if slotData["SkyChaseChecks"] and slotData["SkyChaseChecksHard"] then
        local enabled = slotData["SkyChaseChecks"] ~= 0
        local addHard = slotData["SkyChaseChecksHard"] ~= 0
        local setting = Tracker:FindObjectForCode("SkyChaseChecks")
        if enabled and addHard then
            setting.CurrentStage = 2
        elseif enabled then
            setting.CurrentStage = 1
        else
            setting.CurrentStage = 0
        end
    end
    if slotData["SandHillCheck"] and slotData["SandHillCheckHard"] then
        local enabled = slotData["SandHillCheck"] ~= 0
        local addHard = slotData["SandHillCheckHard"] ~= 0
        local setting = Tracker:FindObjectForCode("SandHillChecks")
        if enabled and addHard then
            setting.CurrentStage = 2
        elseif enabled then
            setting.CurrentStage = 1
        else
            setting.CurrentStage = 0
        end
    end
    if slotData["TwinkleCircuitCheck"] and slotData["MultipleTwinkleCircuitChecks"] then
        local enabled = slotData["TwinkleCircuitCheck"] ~= 0
        local multiple = slotData["MultipleTwinkleCircuitChecks"] ~= 0
        local setting = Tracker:FindObjectForCode("TwinkleCircuitChecks")
        if enabled and multiple then
            setting.CurrentStage = 2
        elseif enabled then
            setting.CurrentStage = 1
        else
            setting.CurrentStage = 0
        end
    end
    
    if slotData["EnemySanity"] then
        local setting = Tracker:FindObjectForCode("Enemysanity")
        setting.Active = slotData["EnemySanity"] ~= 0
    end
    if slotData["CapsuleSanity"] then
        local setting = Tracker:FindObjectForCode("Capsulesanity")
        setting.Active = slotData["CapsuleSanity"] ~= 0
    end
    if slotData["FishSanity"] then
        local setting = Tracker:FindObjectForCode("Fishsanity")
        setting.Active = slotData["FishSanity"] ~= 0
    end
    if slotData["MissableEnemies"] then
        local setting = Tracker:FindObjectForCode("MissableEnemies")
        setting.Active = slotData["MissableEnemies"] ~= 0
    end
    if slotData["MissableCapsules"] then
        local setting = Tracker:FindObjectForCode("MissableCapsules")
        setting.Active = slotData["MissableCapsules"] ~= 0
    end
    if slotData["PinballCapsules"] then
        local setting = Tracker:FindObjectForCode("PinballCapsules")
        setting.Active = slotData["PinballCapsules"] ~= 0
    end
    
    if slotData["PlayableSonic"] then
        local setting = Tracker:FindObjectForCode("SonicPlayable")
        setting.Active = slotData["PlayableSonic"] ~= 0
    end
    if slotData["PlayableTails"] then
        local setting = Tracker:FindObjectForCode("TailsPlayable")
        setting.Active = slotData["PlayableTails"] ~= 0
    end
    if slotData["PlayableKnuckles"] then
        local setting = Tracker:FindObjectForCode("KnucklesPlayable")
        setting.Active = slotData["PlayableKnuckles"] ~= 0
    end
    if slotData["PlayableAmy"] then
        local setting = Tracker:FindObjectForCode("AmyPlayable")
        setting.Active = slotData["PlayableAmy"] ~= 0
    end
    if slotData["PlayableBig"] then
        local setting = Tracker:FindObjectForCode("BigPlayable")
        setting.Active = slotData["PlayableBig"] ~= 0
    end
    if slotData["PlayableGamma"] then
        local setting = Tracker:FindObjectForCode("GammaPlayable")
        setting.Active = slotData["PlayableGamma"] ~= 0
    end
    
    if slotData["SonicStartingArea"] then
        local setting = Tracker:FindObjectForCode("SonicStart")
        setting.CurrentStage = math.tointeger(slotData["SonicStartingArea"]) or 0
    end
    if slotData["TailsStartingArea"] then
        local setting = Tracker:FindObjectForCode("TailsStart")
        setting.CurrentStage = math.tointeger(slotData["TailsStartingArea"]) or 0
    end
    if slotData["KnucklesStartingArea"] then
        local setting = Tracker:FindObjectForCode("KnucklesStart")
        setting.CurrentStage = math.tointeger(slotData["KnucklesStartingArea"]) or 0
    end
    if slotData["AmyStartingArea"] then
        local setting = Tracker:FindObjectForCode("AmyStart")
        setting.CurrentStage = math.tointeger(slotData["AmyStartingArea"]) or 0
    end
    if slotData["BigStartingArea"] then
        local setting = Tracker:FindObjectForCode("BigStart")
        setting.CurrentStage = math.tointeger(slotData["BigStartingArea"]) or 0
    end
    if slotData["GammaStartingArea"] then
        local setting = Tracker:FindObjectForCode("GammaStart")
        setting.CurrentStage = math.tointeger(slotData["GammaStartingArea"]) or 0
    end
    
    if slotData["SonicActionStageMissions"] then
        local setting = Tracker:FindObjectForCode("SonicMissions")
        setting.CurrentStage = math.tointeger(slotData["SonicActionStageMissions"]) or 0
    end
    if slotData["TailsActionStageMissions"] then
        local setting = Tracker:FindObjectForCode("TailsMissions")
        setting.CurrentStage = math.tointeger(slotData["TailsActionStageMissions"]) or 0
    end
    if slotData["KnucklesActionStageMissions"] then
        local setting = Tracker:FindObjectForCode("KnucklesMissions")
        setting.CurrentStage = math.tointeger(slotData["KnucklesActionStageMissions"]) or 0
    end
    if slotData["AmyActionStageMissions"] then
        local setting = Tracker:FindObjectForCode("AmyMissions")
        setting.CurrentStage = math.tointeger(slotData["AmyActionStageMissions"]) or 0
    end
    if slotData["BigActionStageMissions"] then
        local setting = Tracker:FindObjectForCode("BigMissions")
        setting.CurrentStage = math.tointeger(slotData["BigActionStageMissions"]) or 0
    end
    if slotData["GammaActionStageMissions"] then
        local setting = Tracker:FindObjectForCode("GammaMissions")
        setting.CurrentStage = math.tointeger(slotData["GammaActionStageMissions"]) or 0
    end
    
    if slotData["EnemySanityList"] then
        for _, v in pairs(slotData["EnemySanityList"]) do
            if v == 0 then
                local setting = Tracker:FindObjectForCode("SonicEnemysanity")
                setting.Active = false
            end
            if v == 1 then
                local setting = Tracker:FindObjectForCode("TailsEnemysanity")
                setting.Active = false
            end
            if v == 2 then
                local setting = Tracker:FindObjectForCode("KnucklesEnemysanity")
                setting.Active = false
            end
            if v == 3 then
                local setting = Tracker:FindObjectForCode("AmyEnemysanity")
                setting.Active = false
            end
            if v == 4 then
                local setting = Tracker:FindObjectForCode("BigEnemysanity")
                setting.Active = false
            end
            if v == 5 then
                local setting = Tracker:FindObjectForCode("GammaEnemysanity")
                setting.Active = false
            end
        end
    end

    if slotData["CapsuleSanityList"] then
        for _, v in pairs(slotData["CapsuleSanityList"]) do
            if v == 0 then
                local setting = Tracker:FindObjectForCode("SonicLifeCapsulesanity")
                setting.Active = true
            end
            if v == 1 then
                local setting = Tracker:FindObjectForCode("SonicShieldCapsulesanity")
                setting.Active = true
            end
            if v == 2 then
                local setting = Tracker:FindObjectForCode("SonicPowerUpCapsulesanity")
                setting.Active = true
            end
            if v == 3 then
                local setting = Tracker:FindObjectForCode("SonicRingCapsulesanity")
                setting.Active = true
            end
            if v == 4 then
                local setting = Tracker:FindObjectForCode("TailsLifeCapsulesanity")
                setting.Active = true
            end
            if v == 5 then
                local setting = Tracker:FindObjectForCode("TailsShieldCapsulesanity")
                setting.Active = true
            end
            if v == 6 then
                local setting = Tracker:FindObjectForCode("TailsPowerUpCapsulesanity")
                setting.Active = true
            end
            if v == 7 then
                local setting = Tracker:FindObjectForCode("TailsRingCapsulesanity")
                setting.Active = true
            end
            if v == 8 then
                local setting = Tracker:FindObjectForCode("KnucklesLifeCapsulesanity")
                setting.Active = true
            end
            if v == 9 then
                local setting = Tracker:FindObjectForCode("KnucklesShieldCapsulesanity")
                setting.Active = true
            end
            if v == 10 then
                local setting = Tracker:FindObjectForCode("KnucklesPowerUpCapsulesanity")
                setting.Active = true
            end
            if v == 11 then
                local setting = Tracker:FindObjectForCode("KnucklesRingCapsulesanity")
                setting.Active = true
            end
            if v == 12 then
                local setting = Tracker:FindObjectForCode("AmyLifeCapsulesanity")
                setting.Active = true
            end
            if v == 13 then
                local setting = Tracker:FindObjectForCode("AmyShieldCapsulesanity")
                setting.Active = true
            end
            if v == 14 then
                local setting = Tracker:FindObjectForCode("AmyPowerUpCapsulesanity")
                setting.Active = true
            end
            if v == 15 then
                local setting = Tracker:FindObjectForCode("AmyRingCapsulesanity")
                setting.Active = true
            end
            if v == 16 then
                local setting = Tracker:FindObjectForCode("BigLifeCapsulesanity")
                setting.Active = true
            end
            if v == 17 then
                local setting = Tracker:FindObjectForCode("BigShieldCapsulesanity")
                setting.Active = true
            end
            if v == 18 then
                local setting = Tracker:FindObjectForCode("BigPowerUpCapsulesanity")
                setting.Active = true
            end
            if v == 19 then
                local setting = Tracker:FindObjectForCode("BigRingCapsulesanity")
                setting.Active = true
            end
            if v == 20 then
                local setting = Tracker:FindObjectForCode("GammaLifeCapsulesanity")
                setting.Active = true
            end
            if v == 21 then
                local setting = Tracker:FindObjectForCode("GammaShieldCapsulesanity")
                setting.Active = true
            end
            if v == 22 then
                local setting = Tracker:FindObjectForCode("GammaPowerUpCapsulesanity")
                setting.Active = true
            end
            if v == 23 then
                local setting = Tracker:FindObjectForCode("GammaRingCapsulesanity")
                setting.Active = true
            end
        end
    end
    
    if slotData["MissionBlackList"] then
        for i = 1, 60, 1 do
            local code = "AllowMission" .. i
            local setting = Tracker:FindObjectForCode(code)
            if setting then
                setting.Active = true
            end
        end
        for _, v in pairs(slotData["MissionBlackList"]) do
            local code = "AllowMission" .. v
            local setting = Tracker:FindObjectForCode(code)
            if setting then
                setting.Active = false
            end
        end
    end
end
