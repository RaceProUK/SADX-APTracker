namespace RPS.SADX.PopTracker.Generator.Models.Logic;

internal class LevelMission : LogicSpecification
{
    public string Character { get; set; } = string.Empty;

    public string Level { get; set; } = string.Empty;

    public char Mission { get; set; }
}