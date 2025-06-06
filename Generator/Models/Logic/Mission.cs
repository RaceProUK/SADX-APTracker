﻿using System.Diagnostics;

namespace RPS.SADX.PopTracker.Generator.Models.Logic;

[DebuggerDisplay("{Number}: {CardArea} -> {ObjectiveArea} ({Character}")]
internal class Mission : LogicSpecification
{
    public string CardArea { get; set; } = string.Empty;

    public string ObjectiveArea { get; set; } = string.Empty;

    public string Character { get; set; } = string.Empty;

    public int Number { get; set; }
}