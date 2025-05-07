using System.Diagnostics;

namespace RPS.SADX.PopTracker.Generator.Models.Logic;

[DebuggerDisplay("{Character}: {Level} -> {Mission}")]
internal class LevelMission : LogicSpecification
{
    public string Level { get; set; } = string.Empty;

    public string Character { get; set; } = string.Empty;

    public string Mission { get; set; } = string.Empty;
}