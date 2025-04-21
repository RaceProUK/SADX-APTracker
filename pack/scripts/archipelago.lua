ScriptHost:LoadScript("scripts/archipelago/itemMap.lua")
ScriptHost:LoadScript("scripts/archipelago/locationMap.lua")
ScriptHost:LoadScript("scripts/archipelago/settings.lua")

CurrentIndex = -1

function Reset(slotData)
    CurrentIndex = -1

    for _, value in pairs(ItemMap) do
        local code = value[1]
        if code then
            local item = Tracker:FindObjectForCode(code)
            if item then
                print(item.Type)
                if item.Type == "toggle" then
                    item.Active = false
                elseif item.Type == "consumable" then
                    item.AcquiredCount = 0
                end
            end
        end
    end

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
end

function ItemReceived(index, id)
    if index <= CurrentIndex then
        return
    end

    CurrentIndex = index

    local code = ItemMap[id]
    if code then
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
    end
end

Archipelago:AddClearHandler("Reset", Reset)
Archipelago:AddItemHandler("Item Received", ItemReceived)
Archipelago:AddLocationHandler("Location Checked", LocationChecked)