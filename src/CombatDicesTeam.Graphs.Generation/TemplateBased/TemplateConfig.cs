namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

public sealed record TemplateConfig<TNodePayload>(IGraph<GraphWay<TNodePayload>> WayGraph);