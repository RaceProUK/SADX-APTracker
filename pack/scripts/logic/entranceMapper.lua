--Key: SADX APWorld level ID
--Value: Level name and tracker setting stage
EntranceIDMap = {
    ["11"] = { "EmeraldCoast", 1 },
    ["12"] = { "WindyValley", 2 },
    ["13"] = { "Casinopolis", 3 },
    ["14"] = { "IceCap", 4 },
    ["15"] = { "TwinklePark", 5 },
    ["16"] = { "SpeedHighway", 6 },
    ["17"] = { "RedMountain", 7 },
    ["18"] = { "SkyDeck", 8 },
    ["19"] = { "LostWorld", 9 },
    ["20"] = { "FinalEgg", 10 },
    ["21"] = { "HotShelter", 11 }
}

--Key: Vanilla entrance
--Value: Randomised entrance
EntranceMapper = {}

function EntranceMapper:Fill(areaMap)
    for key, value in pairs(areaMap) do
        local entrance = EntranceIDMap[key][1]
        local course = EntranceIDMap[tostring(value)][1]
        EntranceMapper[course] = entrance
    end
end

function EntranceMapper:Reset()
    EntranceMapper["EmeraldCoast"] = nil
    EntranceMapper["WindyValley"] = nil
    EntranceMapper["Casinopolis"] = nil
    EntranceMapper["IceCap"] = nil
    EntranceMapper["TwinklePark"] = nil
    EntranceMapper["SpeedHighway"] = nil
    EntranceMapper["RedMountain"] = nil
    EntranceMapper["SkyDeck"] = nil
    EntranceMapper["LostWorld"] = nil
    EntranceMapper["FinalEgg"] = nil
    EntranceMapper["HotShelter"] = nil
end