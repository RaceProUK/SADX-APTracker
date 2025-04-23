namespace RPS.SADX.PopTracker.Generator.Models.Logic;

internal class Enemy : LogicSpecification
{
    public string Level { get; set; } = string.Empty;

    public string Character { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public int Number { get; set; }
}