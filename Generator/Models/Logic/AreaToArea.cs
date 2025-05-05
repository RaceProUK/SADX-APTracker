using System.Diagnostics;
using QuikGraph;

namespace RPS.SADX.PopTracker.Generator.Models.Logic;

[DebuggerDisplay("{Source} -> {Target}")]
internal class AreaToArea : LogicSpecification, IEdge<string>
{
    public string Character { get; set; } = string.Empty;

    public string AreaFrom { get; set; } = string.Empty;

    public string AreaTo { get; set; } = string.Empty;

    public string Source => AreaFrom;

    public string Target => AreaTo;
}