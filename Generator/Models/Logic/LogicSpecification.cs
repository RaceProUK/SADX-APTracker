using System.Data;

namespace RPS.SADX.PopTracker.Generator.Models.Logic;

internal abstract class LogicSpecification
{
    public LogicRules NormalLogic { get; set; } = [];

    public LogicRules HardLogic { get; set; } = [];

    public LogicRules ExpertDCLogic { get; set; } = [];

    public LogicRules ExpertDXLogic { get; set; } = [];

    public LogicRules ExpertDXPlusLogic { get; set; } = [];

    internal virtual IEnumerable<string>? BuildAccessRules()
    {
        var hardLogic = HardLogic.Any()
            ? HardLogic.Select(_ => $"HardLogic,{string.Join(',', _)}")
            : ["HardLogic"];
        var expertDCLogic = ExpertDCLogic.Any()
            ? ExpertDCLogic.Select(_ => $"ExpertLogicDC,{string.Join(',', _)}")
            : ["ExpertLogicDC"];
        var expertDXLogic = ExpertDXLogic.Any()
            ? ExpertDXLogic.Select(_ => $"ExpertLogicDX,{string.Join(',', _)}")
            : ["ExpertLogicDX"];
        var expertDXPlusLogic = ExpertDXPlusLogic.Any()
            ? ExpertDXPlusLogic.Select(_ => $"ExpertLogicDXPlus,{string.Join(',', _)}")
            : ["ExpertLogicDXPlus"];
        return NormalLogic.Any()
            ? NormalLogic.Select(_ => $"NormalLogic,{string.Join(',', _)}")
                         .Union(hardLogic)
                         .Union(expertDCLogic)
                         .Union(expertDXLogic)
                         .Union(expertDXPlusLogic)
            : default;
    }
}