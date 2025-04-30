function HasItem(itemName)
    local item = Tracker:FindObjectForCode(itemName)
    return item and item.Active
end

function NotHasItem(itemName)
    local item = Tracker:FindObjectForCode(itemName)
    return item and not item.Active
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
    return AccessRules[index]()
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
        local enoughMissions = missionsRequired.AcquiredCount >= missionsRequired.AcquiredCount
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