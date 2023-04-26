using JetBrains.Annotations;

namespace CombatDicesTeam.Graphs.Generation;

[PublicAPI]
public interface IGraphGenerator<TNodePayload>
{
    IGraph<TNodePayload> Create();
}