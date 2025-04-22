namespace RPS.SADX.PopTracker.Generator.Models.Logic;

internal class AreaToLevel
{
    public string Character { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public IEnumerable<string> NormalLogic { get; set; } = [];
    public IEnumerable<string> HardLogic { get; set; } = [];
    public IEnumerable<string> ExpertDCLogic { get; set; } = [];
    public IEnumerable<string> ExpertDXLogic { get; set; } = [];
}