EntranceIDMap = {
    ["2"] = "CityHall-SpeedHighway",
    ["3"] = "CityHall-Chaos0",
    ["8"] = "Casino-Casinopolis",
    ["10"] = "Casino-EggWalker",
    ["19"] = "SSMain-SpeedHighway",
    ["22"] = "Hotel-SSChaoGarden",
    ["23"] = "Hotel-Chaos2",
    ["26"] = "HotelPool-EmeraldCoast",
    ["31"] = "TPLobby-TwinklePark",
    ["32"] = "TPLobby-TwinkleCircuit",
    ["37"] = "MRMain-WindyValley",
    ["39"] = "MRMain-Chaos4",
    ["40"] = "MRMain-EggHornet",
    ["41"] = "MRMain-MRChaoGarden",
    ["42"] = "MRMain-SkyChase1",
    ["45"] = "AngelIsland-RedMountain",
    ["48"] = "IceCave-IceCap",
    ["54"] = "Jungle-LostWorld",
    ["55"] = "Jungle-LostWorldAlt",
    ["57"] = "Jungle-SandHill",
    ["60"] = "FinalEggTower-FinalEgg",
    ["61"] = "FinalEggTower-FinalEggAlt",
    ["62"] = "FinalEggTower-BetaEggViper",
    ["66"] = "ECOutside-SkyChase2",
    ["67"] = "ECOutside-Chaos6ZeroBeta",
    ["74"] = "ECBridge-SkyDeck",
    ["75"] = "ECBridge-SkyChase2",
    ["76"] = "ECBridge-Chaos6ZeroBeta",
    ["91"] = "ECPool-SkyDeck",
    ["97"] = "ECInside-HotShelter",
    ["108"] = "WarpHall-ECChaoGarden"
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