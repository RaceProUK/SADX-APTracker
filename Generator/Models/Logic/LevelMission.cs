namespace RPS.SADX.PopTracker.Generator.Models.Logic;

internal class LevelMission
{
    public string Character { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public char Mission { get; set; }
    public IEnumerable<string> NormalLogic { get; set; } = [];
    public IEnumerable<string> HardLogic { get; set; } = [];
    public IEnumerable<string> ExpertDCLogic { get; set; } = [];
    public IEnumerable<string> ExpertDXLogic { get; set; } = [];
}