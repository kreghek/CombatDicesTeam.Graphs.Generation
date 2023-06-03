namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

/// <summary>
/// Way of a generating graph.
/// </summary>
/// <typeparam name="TNodePayload">Type of graph node data.</typeparam>
/// <param name="WayTemplates">Steps of way.</param>
public sealed record GraphWay<TNodePayload>(IReadOnlyCollection<IGraphTemplate<TNodePayload>> WayTemplates);