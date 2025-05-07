using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RPS.SADX.PopTracker.Generator.Models.Logic;

[DebuggerDisplay("{Area}: {Name}")]
internal partial class FieldEmblem : LogicSpecification
{
    [GeneratedRegex("Playable(([A-Za-z]+))")]
    private static partial Regex Reachability();

    public string Area { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    internal override IEnumerable<string>? BuildAccessRules()
        => base.BuildAccessRules()?.Select(_ => Reachability().Replace(_, $"$CanReach|$1|{Area},Playable$1"));
}