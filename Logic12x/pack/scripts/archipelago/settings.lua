ScriptHost:LoadScript("scripts/archipelago/settingNames.lua")

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
    EntranceMapper:Reset()
end

function ParseSettings(slotData)
    local enemysanityEnabled = false
    local capsulesanityEnabled = false

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
    if slotData["ChaoRacesLevelsToAccessPercentage"] then
        local setting = Tracker:FindObjectForCode(Settings.ChaoRacesAccessLevels)
        setting.AcquiredCount = math.tointeger(slotData["ChaoRacesLevelsToAccessPercentage"]) or 0
    end

    if slotData["LogicLevel"] then
        local setting = Tracker:FindObjectForCode(Settings.LogicLevel)
        setting.CurrentStage = math.tointeger(slotData["LogicLevel"]) or 0
    end
    if slotData["LazyFishing"] then
        local setting = Tracker:FindObjectForCode(Settings.LazyFishing)
        setting.CurrentStage = math.tointeger(slotData["LazyFishing"]) or 0
    end
    if slotData["FieldEmblemChecks"] then
        local setting = Tracker:FindObjectForCode(Settings.FieldEmblemChecks)
        setting.Active = slotData["FieldEmblemChecks"] ~= 0
    end
    if slotData["MissionModeChecks"] then
        local setting = Tracker:FindObjectForCode(Settings.MissionModeChecks)
        setting.Active = slotData["MissionModeChecks"] ~= 0
    end
    if slotData["AutoStartMissions"] then
        local setting = Tracker:FindObjectForCode(Settings.AutoStartMissions)
        setting.Active = slotData["AutoStartMissions"] ~= 0
    end
    if slotData["SecretChaoEggs"] then
        local setting = Tracker:FindObjectForCode(Settings.SecretChaoEggs)
        setting.Active = slotData["SecretChaoEggs"] ~= 0
    end
    if slotData["ChaoRacesChecks"] then
        local setting = Tracker:FindObjectForCode(Settings.ChaoRacesChecks)
        setting.Active = slotData["ChaoRacesChecks"] ~= 0
    end
    
    if slotData["UnifyEggHornet"] then
        local setting = Tracker:FindObjectForCode(Settings.UnifyEggHornet)
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
    if slotData["SkyChaseChecks"] then
        local setting = Tracker:FindObjectForCode(Settings.SkyChaseChecks)
        setting.CurrentStage = math.tointeger(slotData["SkyChaseChecks"]) or 0
    end
    if slotData["SandHillChecks"] then
        local setting = Tracker:FindObjectForCode(Settings.SandHillChecks)
        setting.CurrentStage = math.tointeger(slotData["SandHillChecks"]) or 0
    end
    if slotData["TwinkleCircuitChecks"] then
        local setting = Tracker:FindObjectForCode(Settings.TwinkleCircuitChecks)
        setting.CurrentStage = math.tointeger(slotData["TwinkleCircuitChecks"]) or 0
    end
    
    if slotData["EnemySanity"] then
        local setting = Tracker:FindObjectForCode(Settings.Enemysanity)
        setting.Active = slotData["EnemySanity"] ~= 0
        enemysanityEnabled = setting.Active
    end
    if slotData["CapsuleSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.Capsulesanity)
        setting.Active = slotData["CapsuleSanity"] ~= 0
        capsulesanityEnabled = setting.Active
    end
    if slotData["FishSanity"] then
        local setting = Tracker:FindObjectForCode(Settings.Fishsanity)
        setting.Active = slotData["FishSanity"] ~= 0
    end
    if slotData["MissableEnemies"] then
        local setting = Tracker:FindObjectForCode(Settings.MissableEnemies)
        setting.Active = slotData["MissableEnemies"] ~= 0
    end
    if slotData["MissableCapsules"] then
        local setting = Tracker:FindObjectForCode(Settings.MissableCapsules)
        setting.Active = slotData["MissableCapsules"] ~= 0
    end
    if slotData["PinballCapsules"] then
        local setting = Tracker:FindObjectForCode(Settings.PinballCapsules)
        setting.Active = slotData["PinballCapsules"] ~= 0
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
    if slotData["PlayableBig"] then
        local setting = Tracker:FindObjectForCode(Settings.BigPlayable)
        setting.Active = slotData["PlayableBig"] ~= 0
    end
    if slotData["PlayableGamma"] then
        local setting = Tracker:FindObjectForCode(Settings.GammaPlayable)
        setting.Active = slotData["PlayableGamma"] ~= 0
    end
    
    if slotData["SonicStartingArea"] then
        local setting = Tracker:FindObjectForCode(Settings.SonicStart)
        setting.CurrentStage = math.tointeger(slotData["SonicStartingArea"]) or 0
    end
    if slotData["TailsStartingArea"] then
        local setting = Tracker:FindObjectForCode(Settings.TailsStart)
        setting.CurrentStage = math.tointeger(slotData["TailsStartingArea"]) or 0
    end
    if slotData["KnucklesStartingArea"] then
        local setting = Tracker:FindObjectForCode(Settings.KnucklesStart)
        setting.CurrentStage = math.tointeger(slotData["KnucklesStartingArea"]) or 0
    end
    if slotData["AmyStartingArea"] then
        local setting = Tracker:FindObjectForCode(Settings.AmyStart)
        setting.CurrentStage = math.tointeger(slotData["AmyStartingArea"]) or 0
    end
    if slotData["BigStartingArea"] then
        local setting = Tracker:FindObjectForCode(Settings.BigStart)
        setting.CurrentStage = math.tointeger(slotData["BigStartingArea"]) or 0
    end
    if slotData["GammaStartingArea"] then
        local setting = Tracker:FindObjectForCode(Settings.GammaStart)
        setting.CurrentStage = math.tointeger(slotData["GammaStartingArea"]) or 0
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
    if slotData["BigActionStageMissions"] then
        local setting = Tracker:FindObjectForCode(Settings.BigMissions)
        setting.CurrentStage = math.tointeger(slotData["BigActionStageMissions"]) or 0
    end
    if slotData["GammaActionStageMissions"] then
        local setting = Tracker:FindObjectForCode(Settings.GammaMissions)
        setting.CurrentStage = math.tointeger(slotData["GammaActionStageMissions"]) or 0
    end
    
    if enemysanityEnabled and slotData["EnemySanityList"] then
        for _, v in pairs(slotData["EnemySanityList"]) do
            if v == 0 then
                local setting = Tracker:FindObjectForCode(Settings.SonicEnemysanity)
                setting.Active = true
            end
            if v == 1 then
                local setting = Tracker:FindObjectForCode(Settings.TailsEnemysanity)
                setting.Active = true
            end
            if v == 2 then
                local setting = Tracker:FindObjectForCode(Settings.KnucklesEnemysanity)
                setting.Active = true
            end
            if v == 3 then
                local setting = Tracker:FindObjectForCode(Settings.AmyEnemysanity)
                setting.Active = true
            end
            if v == 4 then
                local setting = Tracker:FindObjectForCode(Settings.BigEnemysanity)
                setting.Active = true
            end
            if v == 5 then
                local setting = Tracker:FindObjectForCode(Settings.GammaEnemysanity)
                setting.Active = true
            end
        end
    end

    if capsulesanityEnabled and slotData["CapsuleSanityList"] then
        for _, v in pairs(slotData["CapsuleSanityList"]) do
            if v == 0 then
                local setting = Tracker:FindObjectForCode(Settings.SonicLifeCapsulesanity)
                setting.Active = true
            end
            if v == 1 then
                local setting = Tracker:FindObjectForCode(Settings.SonicShieldCapsulesanity)
                setting.Active = true
            end
            if v == 2 then
                local setting = Tracker:FindObjectForCode(Settings.SonicPowerUpCapsulesanity)
                setting.Active = true
            end
            if v == 3 then
                local setting = Tracker:FindObjectForCode(Settings.SonicRingCapsulesanity)
                setting.Active = true
            end
            if v == 4 then
                local setting = Tracker:FindObjectForCode(Settings.TailsLifeCapsulesanity)
                setting.Active = true
            end
            if v == 5 then
                local setting = Tracker:FindObjectForCode(Settings.TailsShieldCapsulesanity)
                setting.Active = true
            end
            if v == 6 then
                local setting = Tracker:FindObjectForCode(Settings.TailsPowerUpCapsulesanity)
                setting.Active = true
            end
            if v == 7 then
                local setting = Tracker:FindObjectForCode(Settings.TailsRingCapsulesanity)
                setting.Active = true
            end
            if v == 8 then
                local setting = Tracker:FindObjectForCode(Settings.KnucklesLifeCapsulesanity)
                setting.Active = true
            end
            if v == 9 then
                local setting = Tracker:FindObjectForCode(Settings.KnucklesShieldCapsulesanity)
                setting.Active = true
            end
            if v == 10 then
                local setting = Tracker:FindObjectForCode(Settings.KnucklesPowerUpCapsulesanity)
                setting.Active = true
            end
            if v == 11 then
                local setting = Tracker:FindObjectForCode(Settings.KnucklesRingCapsulesanity)
                setting.Active = true
            end
            if v == 12 then
                local setting = Tracker:FindObjectForCode(Settings.AmyLifeCapsulesanity)
                setting.Active = true
            end
            if v == 13 then
                local setting = Tracker:FindObjectForCode(Settings.AmyShieldCapsulesanity)
                setting.Active = true
            end
            if v == 14 then
                local setting = Tracker:FindObjectForCode(Settings.AmyPowerUpCapsulesanity)
                setting.Active = true
            end
            if v == 15 then
                local setting = Tracker:FindObjectForCode(Settings.AmyRingCapsulesanity)
                setting.Active = true
            end
            if v == 16 then
                local setting = Tracker:FindObjectForCode(Settings.BigLifeCapsulesanity)
                setting.Active = true
            end
            if v == 17 then
                local setting = Tracker:FindObjectForCode(Settings.BigShieldCapsulesanity)
                setting.Active = true
            end
            if v == 18 then
                local setting = Tracker:FindObjectForCode(Settings.BigPowerUpCapsulesanity)
                setting.Active = true
            end
            if v == 19 then
                local setting = Tracker:FindObjectForCode(Settings.BigRingCapsulesanity)
                setting.Active = true
            end
            if v == 20 then
                local setting = Tracker:FindObjectForCode(Settings.GammaLifeCapsulesanity)
                setting.Active = true
            end
            if v == 21 then
                local setting = Tracker:FindObjectForCode(Settings.GammaShieldCapsulesanity)
                setting.Active = true
            end
            if v == 22 then
                local setting = Tracker:FindObjectForCode(Settings.GammaPowerUpCapsulesanity)
                setting.Active = true
            end
            if v == 23 then
                local setting = Tracker:FindObjectForCode(Settings.GammaRingCapsulesanity)
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

    if slotData["EntranceRandomizer"] and slotData["LevelEntranceMap"] then
        EntranceMapper:Fill(slotData["LevelEntranceMap"])
    end
end
