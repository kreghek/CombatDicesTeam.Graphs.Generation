namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

public interface IGraphTemplateContext<TNodePayload>
{
    IList<IGraphNode<TNodePayload>> CurrentWay { get; }
}