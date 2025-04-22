namespace RPS.SADX.PopTracker.Generator.Models.Logic;

internal class Mission
{
    public int Number { get; set; }
    public string Character { get; set; } = string.Empty;
    public string CardArea { get; set; } = string.Empty;
    public string ObjectiveArea { get; set; } = string.Empty;
    public IEnumerable<string> NormalLogic { get; set; } = [];
    public IEnumerable<string> HardLogic { get; set; } = [];
    public IEnumerable<string> ExpertDCLogic { get; set; } = [];
    public IEnumerable<string> ExpertDXLogic { get; set; } = [];
}