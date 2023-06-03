using JetBrains.Annotations;

namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

/// <summary>
/// Template of node.
/// </summary>
/// <typeparam name="TNodePayload">Type of graph node data.</typeparam>
/// <remarks>
/// Use your own implementations to create a graph nodes with concrete data.
/// </remarks>
[PublicAPI]
public interface IGraphTemplate<TNodePayload>
{
    /// <summary>
    /// Validate template can create graph node before creation.
    /// </summary>
    /// <param name="context">Generation context to control graph node creation.</param>
    /// <returns>Returns true then node can be created by template.</returns>
    /// <remarks>
    /// You can use it if you create template wrapper to make variation of final node.
    /// </remarks>
    bool CanCreate(IGraphTemplateContext<TNodePayload> context);

    /// <summary>
    /// Create graph node with concrete data.
    /// </summary>
    /// <param name="context">Generation context to control graph node creation.</param>
    /// <returns>Graph node with concrete data.</returns>
    IGraphNode<TNodePayload> Create(IGraphTemplateContext<TNodePayload> context);
}