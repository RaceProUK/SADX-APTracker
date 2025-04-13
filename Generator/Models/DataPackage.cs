namespace RPS.SADX.PopTracker.Generator.Models;

internal record DataPackage(string Checksum,
                            Dictionary<string, int> ItemNameToId,
                            Dictionary<string, int> LocationNameToId);