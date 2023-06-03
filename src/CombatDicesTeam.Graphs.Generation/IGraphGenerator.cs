using JetBrains.Annotations;

namespace CombatDicesTeam.Graphs.Generation;

/// <summary>
/// Graph generator.
/// </summary>
/// <typeparam name="TNodePayload">Type of graph node data.</typeparam>
[PublicAPI]
public interface IGraphGenerator<TNodePayload>
{
    /// <summary>
    /// Creates graph.
    /// </summary>
    /// <returns>Returns instance of graph.</returns>
    IGraph<TNodePayload> Create();
}