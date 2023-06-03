namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

/// <summary>
/// Config to pass generation parameters to template-based graph generator.
/// </summary>
/// <typeparam name="TNodePayload">Type of graph node data.</typeparam>
/// <param name="WayGraph">Graph of the ways.</param>
public sealed record TemplateConfig<TNodePayload>(IGraph<GraphWay<TNodePayload>> WayGraph);