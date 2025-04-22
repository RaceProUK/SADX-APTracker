namespace RPS.SADX.PopTracker.Generator.Models.Logic;

internal class AreaToArea
{
    public string Character { get; set; } = string.Empty;
    public string AreaFrom { get; set; } = string.Empty;
    public string AreaTo { get; set; } = string.Empty;
    public IEnumerable<string> NormalLogic { get; set; } = [];
    public IEnumerable<string> HardLogic { get; set; } = [];
    public IEnumerable<string> ExpertDCLogic { get; set; } = [];
    public IEnumerable<string> ExpertDXLogic { get; set; } = [];
}