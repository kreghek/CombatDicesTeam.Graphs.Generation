namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

public interface IGraphTemplate<TNodePayload>
{
    IGraphNode<TNodePayload> Create(IGraphTemplateContext<TNodePayload> context);

    bool CanCreate(IGraphTemplateContext<TNodePayload> context);
}