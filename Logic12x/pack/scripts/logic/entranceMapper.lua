EntranceIDMap = {
    ["2"] = "SpeedHighwayK",
    ["3"] = "Chaos0",
    ["8"] = "Casinopolis",
    ["10"] = "EggWalker",
    ["19"] = "SpeedHighway",
    ["22"] = "SSChaoGarden",
    ["23"] = "Chaos2",
    ["26"] = "EmeraldCoast",
    ["31"] = "TwinklePark",
    ["32"] = "TwinkleCircuit",
    ["37"] = "WindyValley",
    ["39"] = "Chaos4",
    ["40"] = "EggHornet",
    ["41"] = "MRChaoGarden",
    ["42"] = "SkyChase1",
    ["45"] = "RedMountain",
    ["48"] = "IceCap",
    ["54"] = "LostWorld",
    ["55"] = "LostWorldK",
    ["57"] = "SandHill",
    ["60"] = "FinalEgg",
    ["61"] = "FinalEggG",
    ["62"] = "EggViperBeta",
    ["66"] = "SkyChase2A",
    ["67"] = "Chaos6ZeroBetaA",
    ["74"] = "SkyDeck",
    ["75"] = "SkyChase2B",
    ["76"] = "Chaos6ZeroBetaB",
    ["91"] = "SkyDeckK",
    ["97"] = "HotShelter",
    ["108"] = "ECChaoGarden"
}

--Key: Vanilla entrance
--Value: Randomised entrance
EntranceMapper = {}

function EntranceMapper:Fill(areaMap)
    for key, value in pairs(areaMap) do
        local entrance = EntranceIDMap[key]
        local course = EntranceIDMap[tostring(value)]
        EntranceMapper[course] = entrance
    end
end

function EntranceMapper:Reset()
    EntranceMapper = {}
end