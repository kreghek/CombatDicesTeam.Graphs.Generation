namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

/// <summary>
/// Simple implementation of generation context.
/// </summary>
public sealed record  GraphTemplateContext<TNodePayload>(IReadOnlyList<IGraphNode<TNodePayload>> CurrentWay): IGraphTemplateContext<TNodePayload>;