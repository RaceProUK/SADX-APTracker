namespace RPS.SADX.PopTracker.Generator.Models.Logic;

internal class Mission : LogicSpecification
{
    public string Character { get; set; } = string.Empty;

    public string CardArea { get; set; } = string.Empty;

    public string ObjectiveArea { get; set; } = string.Empty;

    public int Number { get; set; }
}