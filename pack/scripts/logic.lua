--Invert the logical state of an item
function InvertItem(itemName)
    local item = Tracker:FindObjectForCode(itemName)
    return item and not item.Active
end