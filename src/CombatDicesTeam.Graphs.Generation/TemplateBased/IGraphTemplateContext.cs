using JetBrains.Annotations;

namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

/// <summary>
/// Context of graph generation.
/// </summary>
/// <typeparam name="TNodePayload">Type of node data.</typeparam>
/// <remarks>
/// Use it to make a desicions in your own implementation of templates.
/// For example, you can avoid duplicates in graph if current way contains some data yet.
/// </remarks>
[PublicAPI]
public interface IGraphTemplateContext<TNodePayload>
{
    IReadOnlyList<IGraphNode<TNodePayload>> CurrentWay { get; }
}