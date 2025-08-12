ScriptHost:LoadScript("scripts/logic/accessRules.lua")
ScriptHost:LoadScript("scripts/logic/reachRules.lua")
ScriptHost:LoadScript("scripts/logic/entranceAreas.lua")
ScriptHost:LoadScript("scripts/logic/entranceMapper.lua")

function HasItem(itemName)
    local item = Tracker:FindObjectForCode(itemName)
    return item and item.Active
end

function NotHasItem(itemName)
    local item = Tracker:FindObjectForCode(itemName)
    return item and not item.Active
end

function IsLazyFishingLevel(level)
    level = math.tointeger(level)

    local setting = Tracker:FindObjectForCode("LazyFishing")
    if setting == nil or setting.CurrentStage < level then
        return AccessibilityLevel.Normal
    elseif setting.CurrentStage >= level then
        if HasItem("PowerRod") then
            return AccessibilityLevel.Normal
        else
            return AccessibilityLevel.SequenceBreak
        end
    end
end

function CanReach(character, target, isMissionCardCheck)
    local setting = Tracker:FindObjectForCode("AutoStartMissions")
    if setting and setting.Active and isMissionCardCheck then
        return true
    end

    local logicSetting = Tracker:FindObjectForCode("LogicLevel")
    local startSetting = Tracker:FindObjectForCode(character .. "Start")
    if logicSetting == nil or startSetting == nil then
        return true
    end

    local logicLevel = logicSetting.CurrentStage
    local origin = ""
    if startSetting.CurrentStage == 0 then
        origin = "StationSquareMain"
    elseif startSetting.CurrentStage == 1 then
        origin = "Station"
    elseif startSetting.CurrentStage == 2 then
        origin = "Hotel"
    elseif startSetting.CurrentStage == 3 then
        origin = "Casino"
    elseif startSetting.CurrentStage == 4 then
        origin = "TwinkleParkLobby"
    elseif startSetting.CurrentStage == 5 then
        origin = "MysticRuinsMain"
    elseif startSetting.CurrentStage == 6 then
        origin = "AngelIsland"
    elseif startSetting.CurrentStage == 7 then
        origin = "Jungle"
    elseif startSetting.CurrentStage == 8 then
        origin = "EggCarrierOutside"
    elseif startSetting.CurrentStage == 9 then
        origin = "EggCarrierInside"
    elseif startSetting.CurrentStage == 10 then
        origin = "EggCarrierFrontDeck"
    end

    if origin == target then
        return true
    else
        local index = character .. " - " .. origin .." - " .. target .. " - " .. logicLevel
        return ReachRules[index]()
    end
end

function CanAccess(character, target)
    local setting = Tracker:FindObjectForCode("LogicLevel")
    if setting == nil then
        return true
    end

    local newTarget = EntranceMapper[target]
    if newTarget ~= nil then
        target = newTarget
    end

    local logicLevel = setting.CurrentStage
    local index = character .. " - " .. target .. " - " .. logicLevel
    local area = EntranceAreas[character .. " - " .. target]
    return CanReach(character, area) and AccessRules[index]()
end

function HasMetGoalCriteria()
    local emblemsRequired = Tracker:FindObjectForCode("EmblemsRequired")
    local emblemsObtained = Tracker:FindObjectForCode("Emblems")
    local levelsRequired = Tracker:FindObjectForCode("LevelsRequired")
    local levelsBeaten = Tracker:FindObjectForCode("LevelsBeaten")
    local emeraldsRequired = Tracker:FindObjectForCode("EmeraldsRequired")
    local whiteChaosEmerald = Tracker:FindObjectForCode("WhiteChaosEmerald")
    local redChaosEmerald = Tracker:FindObjectForCode("RedChaosEmerald")
    local cyanChaosEmerald = Tracker:FindObjectForCode("CyanChaosEmerald")
    local purpleChaosEmerald = Tracker:FindObjectForCode("PurpleChaosEmerald")
    local greenChaosEmerald = Tracker:FindObjectForCode("GreenChaosEmerald")
    local yellowChaosEmerald = Tracker:FindObjectForCode("YellowChaosEmerald")
    local blueChaosEmerald = Tracker:FindObjectForCode("BlueChaosEmerald")
    local bossesRequired = Tracker:FindObjectForCode("BossesRequired")
    local bossesBeaten = Tracker:FindObjectForCode("BossesBeaten")
    local missionsRequired = Tracker:FindObjectForCode("MissionsRequired")
    local missionsBeaten = Tracker:FindObjectForCode("MissionsBeaten")
    local chaoRacesRequired = Tracker:FindObjectForCode("chaoRacesRequired")
    local chaoRacesWon = Tracker:FindObjectForCode("ChaoRacesWon")

    if emblemsRequired and emblemsObtained and
       levelsRequired and levelsBeaten and
       emeraldsRequired and whiteChaosEmerald and
       redChaosEmerald and cyanChaosEmerald and
       purpleChaosEmerald and greenChaosEmerald and
       yellowChaosEmerald and blueChaosEmerald and
       bossesRequired and bossesBeaten and
       missionsRequired and missionsBeaten and
       chaoRacesRequired and chaoRacesWon then
        local enoughEmblems = emblemsObtained.AcquiredCount >= emblemsRequired.AcquiredCount
        local enoughLevels = levelsBeaten.AcquiredCount >= levelsRequired.AcquiredCount
        local enoughBosses = bossesBeaten.AcquiredCount >= bossesRequired.AcquiredCount
        local enoughMissions = missionsBeaten.AcquiredCount >= missionsRequired.AcquiredCount
        local enoughChaoRaces = chaoRacesWon.AcquiredCount >= chaoRacesRequired.AcquiredCount
        local enoughEmeralds = (whiteChaosEmerald.Active and
                                redChaosEmerald.Active and
                                cyanChaosEmerald.Active and
                                purpleChaosEmerald.Active and
                                greenChaosEmerald.Active and
                                yellowChaosEmerald.Active and
                                blueChaosEmerald.Active) or not emeraldsRequired.Active
        return enoughEmblems and enoughLevels and enoughBosses and enoughMissions and enoughChaoRaces and enoughEmeralds
    else
        return false
    end
end