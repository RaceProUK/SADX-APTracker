--Key: SADX APWorld level ID
--Value: Level name and tracker setting stage
EntranceIDMap = {
    ["11"] = { "Emerald Coast", 1 },
    ["12"] = { "Windy Valley", 2 },
    ["13"] = { "Casinopolis", 3 },
    ["14"] = { "Ice Cap", 4 },
    ["15"] = { "Twinkle Park", 5 },
    ["16"] = { "Speed Highway", 6 },
    ["17"] = { "Red Mountain", 7 },
    ["18"] = { "Sky Deck", 8 },
    ["19"] = { "Lost World", 9 },
    ["20"] = { "Final Egg", 10 },
    ["21"] = { "Hot Shelter", 11 }
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
    EntranceMapper["Emerald Coast"] = nil
    EntranceMapper["Windy Valley"] = nil
    EntranceMapper["Casinopolis"] = nil
    EntranceMapper["Ice Cap"] = nil
    EntranceMapper["Twinkle Park"] = nil
    EntranceMapper["Speed Highway"] = nil
    EntranceMapper["Red Mountain"] = nil
    EntranceMapper["Sky Deck"] = nil
    EntranceMapper["Lost World"] = nil
    EntranceMapper["Final Egg"] = nil
    EntranceMapper["Hot Shelter"] = nil
end