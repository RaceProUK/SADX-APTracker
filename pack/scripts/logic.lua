function HasItem(itemName)
    local item = Tracker:FindObjectForCode(itemName)
    return item and item.Active
end

function NotHasItem(itemName)
    local item = Tracker:FindObjectForCode(itemName)
    return item and not item.Active
end