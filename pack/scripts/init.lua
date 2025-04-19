Tracker:AddLocations("locations/bosses.json")
Tracker:AddLocations("locations/capsules.json")
Tracker:AddLocations("locations/chaoEggs.json")
Tracker:AddLocations("locations/chaoRaces.json")
Tracker:AddLocations("locations/emblems.json")
Tracker:AddLocations("locations/enemies.json")
Tracker:AddLocations("locations/levels.json")
Tracker:AddLocations("locations/missions.json")
Tracker:AddLocations("locations/subgames.json")
Tracker:AddLocations("locations/upgrades.json")

Tracker:AddItems("items/characters.json")
Tracker:AddItems("items/collectibles.json")
Tracker:AddItems("items/emeralds.json")
Tracker:AddItems("items/goals.json")
Tracker:AddItems("items/keys.json")
Tracker:AddItems("items/upgrades.json")
Tracker:AddItems("items/settings.json")

Tracker:AddMaps("layouts/maps.json")

Tracker:AddLayouts("layouts/progression.json")
Tracker:AddLayouts("layouts/characters.json")
Tracker:AddLayouts("layouts/keys.json")
Tracker:AddLayouts("layouts/settings.json")
Tracker:AddLayouts("layouts/layout.json")
Tracker:AddLayouts("layouts/tracker.json")

ScriptHost:LoadScript("scripts/logic.lua")
--if PopVersion and PopVersion >= "0.18.0" then
--    ScriptHost:LoadScript("scripts/archipelago.lua")
--end