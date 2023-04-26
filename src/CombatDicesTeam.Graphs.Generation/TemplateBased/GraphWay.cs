namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

public sealed record GraphWay<TNodePayload>(IReadOnlyCollection<IGraphTemplate<TNodePayload>> WayTemplates);