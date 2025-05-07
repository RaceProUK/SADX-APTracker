using System.Diagnostics;

namespace RPS.SADX.PopTracker.Generator.Models.Logic;

[DebuggerDisplay("{Character}: {Area} -> {Upgrade}")]
internal class UpgradeItem : LogicSpecification
{
    public string Character { get; set; } = string.Empty;

    public string Area { get; set; } = string.Empty;

    public string Upgrade { get; set; } = string.Empty;
}