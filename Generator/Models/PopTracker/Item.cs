namespace RPS.SADX.PopTracker.Generator.Models.PopTracker;

internal abstract record Item(string Name,
                              string Type,
                              string Img,
                              string Codes);

internal record ToggleItem(string Name,
                           string Img,
                           string Codes)
    : Item(Name, "toggle", Img, Codes);

internal record CollectibleItem(string Name,
                                string Img,
                                string Codes,
                                int MaxQuantity,
                                int InitialQuantity = 0,
                                int OverlayFontSize = 16)
    : Item(Name, "consumable", Img, Codes);