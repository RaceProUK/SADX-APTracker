function StartWatches()
    ScriptHost:AddWatchForCode("VersionMismatch", "VersionMismatch", ToggleVersionMismatch)
end

function ToggleVersionMismatch(code)
    local active = Tracker:FindObjectForCode(code).Active
    if active then
        Tracker:AddLayouts("layouts/versionMismatch.json")
    else
        Tracker:AddLayouts("layouts/tracker.json")
    end
end