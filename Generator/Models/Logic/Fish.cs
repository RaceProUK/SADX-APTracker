using System.Diagnostics;

namespace RPS.SADX.PopTracker.Generator.Models.Logic;

[DebuggerDisplay("{Level}: {Type}")]
internal class Fish : LogicSpecification
{
    public string Level { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
}