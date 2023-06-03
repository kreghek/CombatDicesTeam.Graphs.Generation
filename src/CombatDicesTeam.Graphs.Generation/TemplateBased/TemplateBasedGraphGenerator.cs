using JetBrains.Annotations;

namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

/// <summary>
/// Implemetation of graph generator based on templates and ways.
/// </summary>
/// <typeparam name="TNodePayload">Type of graph node data.</typeparam>
[PublicAPI]
public sealed class TemplateBasedGraphGenerator<TNodePayload>: IGraphGenerator<TNodePayload>
{
    private readonly TemplateConfig<TNodePayload> _config;

    public TemplateBasedGraphGenerator(TemplateConfig<TNodePayload> config)
    {
        _config = config;
    }

    /// <inheritdoc/>
    public IGraph<TNodePayload> Create()
    {
        var wayGraph = _config.WayGraph;

        var materializedWays = wayGraph.GetAllNodes().Select(x => new { Way = x, Nodes = TemplateBasedGraphGenerator<TNodePayload>.CreateWayNodes(x) }).ToArray();
        
        var graph = new DirectedGraph<TNodePayload>();

        foreach (var materializedWay in materializedWays)
        {
            ConnectNodes(graph, materializedWay.Nodes);
        }

        foreach (var materializedWay in materializedWays)
        {
            var sourceWayNode = materializedWay.Nodes.Last();
            
            var nextWays = wayGraph.GetNext(materializedWay.Way);

            foreach (var nextWay in nextWays)
            {
                var nextWayNodes = materializedWays.Single(x => x.Way == nextWay);

                var targetWayNode = nextWayNodes.Nodes.First();
                
                graph.ConnectNodes(sourceWayNode, targetWayNode);
            }
        }

        return graph;
    }

    private static void ConnectNodes(IGraph<TNodePayload> graph, IList<IGraphNode<TNodePayload>> nodes)
    {
        IGraphNode<TNodePayload>? prevNode = null;
        foreach (var graphNode in nodes)
        {
            graph.AddNode(graphNode);
            if (prevNode is not null)
            {
                graph.ConnectNodes(prevNode, graphNode);
            }

            prevNode = graphNode;
        }
    }

    private static IList<IGraphNode<TNodePayload>> CreateWayNodes(IGraphNode<GraphWay<TNodePayload>> way)
    {
        var list = new List<IGraphNode<TNodePayload>>();
        foreach (var wayTemplate in way.Payload.WayTemplates)
        {
            var context = new GraphTemplateContext<TNodePayload>(list);
            var node = wayTemplate.Create(context);
            list.Add(node);
        }

        return list;
    }
}