namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class AccessRulesGenerator
{
    internal static IEnumerable<string> Areas { get; private set; } = [];

    internal static IEnumerable<string> Levels { get; private set; } = [];

    internal static IEnumerable<string> Characters { get; private set; } = [];

    internal static async Task Generate()
    {
        var logic = await LogicLoader.LoadForConnections().ToListAsync();
        Areas = [.. logic.Select(_ => _.AreaFrom).Distinct()];
        Characters = [.. logic.Select(_ => _.Character).Distinct()];
    }
}