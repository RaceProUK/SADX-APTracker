namespace RPS.SADX.PopTracker.Generator.Models.PopTracker;

internal abstract record Item(string Name,
                              string Type,
                              string Codes);

internal abstract record ImageItem(string Name,
                                   string Type,
                                   string Codes,
                                   string Img)
    : Item(Name, Type, Codes);

internal record ToggleItem(string Name,
                           string Codes,
                           string Img)
    : ImageItem(Name, "toggle", Codes, Img);

internal record CollectibleItem(string Name,
                                string Codes,
                                string Img,
                                int MaxQuantity,
                                int InitialQuantity = 0,
                                int OverlayFontSize = 16)
    : ImageItem(Name, "consumable", Codes, Img);

internal record ProgressiveItem(string Name,
                                string Codes,
                                bool Loop,
                                bool AllowDisabled,
                                int InitialStageIdx,
                                IEnumerable<ProgressiveItemStage> Stages)
    : Item(Name, "progressive", Codes);

internal record ProgressiveItemStage(string Name, string Codes, string Img);