ScriptHost:LoadScript("scripts/logic/routes.lua")
ScriptHost:LoadScript("scripts/logic/accessRules.lua")
ScriptHost:LoadScript("scripts/logic/entranceMapper.lua")

OriginMap = {
    [0] = "CityHall",
    [1] = "Station",
    [2] = "Casino",
    [3] = "Sewers",
    [4] = "SSMain",
    [5] = "TPTunnel",
    [6] = "Hotel",
    [7] = "HotelPool",
    [8] = "TPLobby",
    [9] = "MRMain",
    [10] = "AngelIsland",
    [11] = "IceCave",
    [12] = "PastAltar",
    [13] = "PastMain",
    [14] = "Jungle",
    [15] = "FinalEggTower",
    [16] = "Outside",
    [17] = "ECBridge",
    [18] = "ECDeck",
    [19] = "CaptainRoom",
    [20] = "ECPool",
    [21] = "Arsenal",
    [22] = "ECInside",
    [23] = "HedgehogHammer",
    [24] = "PrisonHall",
    [25] = "WaterTank",
    [26] = "WarpHall",
}

function HasItem(itemName)
    local item = Tracker:FindObjectForCode(itemName)
    return item and item.Active
end

function NotHasItem(itemName)
    local item = Tracker:FindObjectForCode(itemName)
    return item and not item.Active
end

function LazyFishingCheck(level)
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

function CanAccess(character, target, isMissionCardCheck)
    print(character .. " - " .. target)

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
    local origin = OriginMap[startSetting.CurrentStage]
    if origin == target then
        return true
    end

    local route = Routes[origin .. " - " .. target]
    if route == nil then
        return true
    end

    for _, connections in pairs(route) do
        local passable = true
        for _, connection in pairs(connections) do
            local rule = AccessRules[character .. " - " .. connection .. " - " .. logicLevel]
            if rule ~= null then
                passable = passable and rule()
            end
        end
        if passable then
            return true
        end
    end

    return false
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