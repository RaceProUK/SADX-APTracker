namespace RPS.SADX.PopTracker.Generator.Models.PopTracker;

internal record Location(string Name,
                         IEnumerable<MapLocation> MapLocations,
                         IEnumerable<Section> Sections,
                         IEnumerable<string>? AccessRules = default,
                         IEnumerable<string>? VisibilityRules = default);

internal record MapLocation(string Map, int X, int Y, int Size, int BorderThickness);

internal record Section(string Name,
                        int ItemCount = 1,
                        IEnumerable<string>? AccessRules = default,
                        IEnumerable<string>? VisibilityRules = default);