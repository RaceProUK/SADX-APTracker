namespace RPS.SADX.PopTracker.Generator.Models.PopTracker;

internal abstract record Item(string Name,
                              string Type,
                              string Image,
                              string Codes);

internal record ToggleItem(string Name,
                           string Image,
                           string Codes)
    : Item(Name, "toggle", Image, Codes);

internal record CollectibleItem(string Name,
                                string Image,
                                string Codes,
                                int MaxQuantity,
                                int InitialQuantity = 0,
                                int OverlayFontSize = 16)
    : Item(Name, "consumable", Image, Codes);