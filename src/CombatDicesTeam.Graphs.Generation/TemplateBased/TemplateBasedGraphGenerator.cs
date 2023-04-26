using JetBrains.Annotations;

namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

[PublicAPI]
public sealed class TemplateBasedGraphGenerator<TNodePayload>: IGraphGenerator<TNodePayload>
{
    private readonly TemplateConfig<TNodePayload> _config;

    public TemplateBasedGraphGenerator(TemplateConfig<TNodePayload> config)
    {
        _config = config;
    }

    public IGraph<TNodePayload> Create()
    {
        var templateWay = _config.WayTemplates;
        
        // Create campaign way
        var way = new List<IGraphNode<TNodePayload>>();
        
        foreach (var template in templateWay)
        {
            var context = new GraphTemplateContext<TNodePayload>(way);
            var node = template.Create(context);
            way.Add(node);
        }

        var terminalContext = new GraphTemplateContext<TNodePayload>(way);
        var terminalNode = _config.TerminalTemplate.Create(terminalContext);
        way.Add(terminalNode);

        var graph = new Graph<TNodePayload>();
        IGraphNode<TNodePayload>? prevNode = null;
        foreach (var node in way)
        {
            graph.AddNode(node);

            if (prevNode is not null)
            {
                graph.ConnectNodes(prevNode, node);
            }

            prevNode = node;
        }

        return graph;
    }
}