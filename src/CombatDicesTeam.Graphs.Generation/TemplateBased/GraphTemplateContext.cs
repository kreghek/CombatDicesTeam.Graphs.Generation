namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

public sealed record  GraphTemplateContext<TNodePayload>(IList<IGraphNode<TNodePayload>> CurrentWay): IGraphTemplateContext<TNodePayload>;