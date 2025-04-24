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