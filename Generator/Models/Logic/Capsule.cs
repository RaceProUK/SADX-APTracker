using System.Diagnostics;

namespace RPS.SADX.PopTracker.Generator.Models.Logic;

[DebuggerDisplay("{Character}: {Level} -> {Type} ({Number})")]
internal class Capsule : LogicSpecification
{
    public string Level { get; set; } = string.Empty;

    public string Character { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public int Number { get; set; }
}