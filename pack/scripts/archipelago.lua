ScriptHost:LoadScript("scripts/archipelago/itemMap.lua")
ScriptHost:LoadScript("scripts/archipelago/locationMap.lua")
ScriptHost:LoadScript("scripts/archipelago/settings.lua")

CurrentIndex = -1

function Reset(slotData)
    Tracker.BulkUpdate = true
    CurrentIndex = -1

    ResetSettings()

    --Accumulated Items
    Tracker:FindObjectForCode("Emblems").AcquiredCount = 0
    Tracker:FindObjectForCode("LevelsBeaten").AcquiredCount = 0
    Tracker:FindObjectForCode("BossesBeaten").AcquiredCount = 0
    Tracker:FindObjectForCode("MissionsBeaten").AcquiredCount = 0
    Tracker:FindObjectForCode("ChaoRacesWon").AcquiredCount = 0

    --Auto-tracked Items
    for _, value in pairs(ItemMap) do
        local code = tostring(value)
        if code then
            local item = Tracker:FindObjectForCode(code)
            if item then
                if item.Type == "toggle" then
                    item.Active = false
                elseif item.Type == "consumable" then
                    item.AcquiredCount = 0
                end
            end
        end
    end

    --Locations
    for _, value in pairs(LocationMap) do
        local area = value[1]
        local section = value[2]
        local address = "@" .. area .. "/" .. section
        local location = Tracker:FindObjectForCode(address)
        if location then
            location.AvailableChestCount = location.ChestCount
        end
    end

    if slotData ~= nil then
        ParseSettings(slotData)
    end

    Tracker.BulkUpdate = false
end

function ItemReceived(index, id)
    if index <= CurrentIndex then
        return
    end

    CurrentIndex = index

    local code = ItemMap[id]
    if code then
        if code == "Emblem" then code = "Emblems" end

        local item = Tracker:FindObjectForCode(tostring(code))
        if item then
            if item.Type == "toggle" then
                item.Active = true
            elseif item.Type == "consumable" then
                item.AcquiredCount = item.AcquiredCount + item.Increment
            end
        end
    end
end

function LocationChecked(id)
    local mapping = LocationMap[id]
    if not mapping then
        return
    end

    local area = mapping[1]
    local section = mapping[2]
    local address = "@" .. area .. "/" .. section
    local location = Tracker:FindObjectForCode(address)
    if location then
        location.AvailableChestCount = location.AvailableChestCount - 1

        if section == "Mission C" and not string.find(area, "Mission") then
            local item = Tracker:FindObjectForCode("LevelsBeaten")
            if item then
                item.AcquiredCount = item.AcquiredCount + item.Increment
            end
        elseif string.find(area, "Boss") then
            local item = Tracker:FindObjectForCode("BossesBeaten")
            if item then
                item.AcquiredCount = item.AcquiredCount + item.Increment
            end
        elseif string.find(area, "Mission") then
            local item = Tracker:FindObjectForCode("MissionsBeaten")
            if item then
                item.AcquiredCount = item.AcquiredCount + item.Increment
            end
        elseif string.find(area, "Chao Race") then
            local item = Tracker:FindObjectForCode("ChaoRacesWon")
            if item then
                item.AcquiredCount = item.AcquiredCount + item.Increment
            end
        end
    end
end

Archipelago:AddClearHandler("Reset", Reset)
Archipelago:AddItemHandler("Item Received", ItemReceived)
Archipelago:AddLocationHandler("Location Checked", LocationChecked)