namespace RPS.SADX.PopTracker.Generator.Models.Logic;

internal class Mission
{
    public string Character { get; set; } = string.Empty;
    public string CardArea { get; set; } = string.Empty;
    public string ObjectiveArea { get; set; } = string.Empty;
    public int Number { get; set; }
    public IEnumerable<string> NormalLogic { get; set; } = [];
    public IEnumerable<string> HardLogic { get; set; } = [];
    public IEnumerable<string> ExpertDCLogic { get; set; } = [];
    public IEnumerable<string> ExpertDXLogic { get; set; } = [];
}