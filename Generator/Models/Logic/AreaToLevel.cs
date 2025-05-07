using System.Diagnostics;

namespace RPS.SADX.PopTracker.Generator.Models.Logic;

[DebuggerDisplay("{Character}: {Area} -> {Level}")]
internal class AreaToLevel : LogicSpecification
{
    public string Character { get; set; } = string.Empty;

    public string Area { get; set; } = string.Empty;

    public string Level { get; set; } = string.Empty;
}