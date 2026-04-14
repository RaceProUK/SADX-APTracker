namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class AccessRulesGenerator
{
#pragma warning disable S1192
    internal static IEnumerable<(string, string)> Entrances { get; } =
    [
        ("CityHall", "SpeedHighway"),
        ("CityHall", "Chaos0"),
        ("Casino", "Casinopolis"),
        ("Casino", "EggWalker"),
        ("SSMain", "SpeedHighway"),
        ("Hotel", "SSChaoGarden"),
        ("Hotel", "Chaos2"),
        ("HotelPool", "EmeraldCoast"),
        ("TPLobby", "TwinklePark"),
        ("TPLobby", "TwinkleCircuit"),
        ("MRMain", "WindyValley"),
        ("MRMain", "Chaos4"),
        ("MRMain", "EggHornet"),
        ("MRMain", "MRChaoGarden"),
        ("MRMain", "SkyChase1"),
        ("AngelIsland", "RedMountain"),
        ("IceCave", "IceCap"),
        ("Jungle", "LostWorld"),
        ("Jungle", "LostWorldAlt"),
        ("Jungle", "SandHill"),
        ("FinalEggTower", "FinalEgg"),
        ("FinalEggTower", "FinalEggAlt"),
        ("FinalEggTower", "BetaEggViper"),
        ("ECOutside", "SkyChase2"),
        ("ECOutside", "Chaos6ZeroBeta"),
        ("ECBridge", "SkyDeck"),
        ("ECBridge", "SkyChase2"),
        ("ECBridge", "Chaos6ZeroBeta"),
        ("ECPool", "SkyDeck"),
        ("ECInside", "HotShelter"),
        ("WarpHall", "ECChaoGarden"),
    ];
#pragma warning restore S1192

    internal static IEnumerable<string> Characters { get; private set; } = [];

    internal static IEnumerable<string> Levels { get => Entrances.Select(_ => _.Item2).Distinct(); }

    internal static async Task Generate()
    {
        var logic = await LogicLoader.LoadForConnections().ToListAsync();
        Characters = [.. logic.Select(_ => _.Character).Distinct()];
    }
}