# CombatDicesTeam.Graphs.Generation

The simplest lightweight library to generate graphs.

## Usage

Generation by template of ways. The ways is easily to generate that whole graph.

```c#
// 1. Create graph of ways.
// Details of the method see below.

var wayGraph = CreateWaysGraph();

// 2. Create final graph using ways.
// There is custom data stored in the ILocationData implementations.

var graphGenerator =
	new TemplateBasedGraphGenerator<ILocationData>(
		new TemplateConfig<ILocationData>(wayGraph));

var worldGraph = graphGenerator.Create();
```

Example of `CreateWaysGraph()` method:

```c#
public IGraph<GraphWay<ILocationData>> CreateWaysGraph()
{
    var wayGraph = new Graph<GraphWay<ILocationData>>();

	var way1Templates = new IGraphTemplate<ILocationData>[]
	{
		new CombatLocationTemplate(),
		new RestLocationTemplate(),
		new CrisisLocationTemplate()
	};

	var way2Templates = new IGraphTemplate<ILocationData>[]
	{
	    new CrisisLocationTemplate(),
		new CombatLocationTemplate()
	};

	var way3Templates = new IGraphTemplate<ILocationData>[]
	{
		new BossLocationTemplate()
	};

	var regular1Way = new GraphWay<ILocationData>(way1Templates);
	var way11Node = new GraphNode<GraphWay<ILocationData>>(regular1Way);
	var way12Node = new GraphNode<GraphWay<ILocationData>>(regular1Way);
	var way13Node = new GraphNode<GraphWay<ILocationData>>(regular1Way);

	var regular2Way = new GraphWay<ILocationData>(way2Templates);
	var way2Node = new GraphNode<GraphWay<ILocationData>>(regular2Way);

	var regular3Way = new GraphWay<ILocationData>(way3Templates);
	var way31Node = new GraphNode<GraphWay<ILocationData>>(regular3Way);
	var way32Node = new GraphNode<GraphWay<ILocationData>>(regular3Way);

	var rewardNode = new GraphNode<GraphWay<ILocationData>>(new GraphWay<ILocationData>(new[]
	{
		new RewardLocationTemplate(_services)
	}));

	wayGraph.AddNode(way11Node);
	wayGraph.AddNode(way12Node);
	wayGraph.AddNode(way13Node);

	wayGraph.AddNode(way2Node);

	wayGraph.ConnectNodes(way11Node, way2Node);
	wayGraph.ConnectNodes(way12Node, way2Node);
	wayGraph.ConnectNodes(way13Node, way2Node);

	wayGraph.AddNode(way31Node);
	wayGraph.AddNode(way32Node);

	wayGraph.ConnectNodes(way2Node, way31Node);
	wayGraph.ConnectNodes(way2Node, way32Node);

	wayGraph.AddNode(rewardNode);

	wayGraph.ConnectNodes(way31Node, rewardNode);
	wayGraph.ConnectNodes(way32Node, rewardNode);

	return wayGraph;
}
```

Finally, create templates. There is example:

```c#
class CombatLocationTemplate: IGraphTemplate<ILocationData>
{
    IGraphNode<TNodePayload> Create(IGraphTemplateContext<ILocationData> context)
	{
	    // Create instance of combat implementation of ILocationData.
	}

    bool CanCreate(IGraphTemplateContext<ILocationData> context)
	{
		return true;
	}
}
```

## Motivation

The library was made for the indie game devs, so as not to pull monstrous enterprise solutions for working with graphs into small pet-games.

## Authors and acknowledgment

*   [KregHEk](https://github.com/kreghek)

## Contributing

Feel free to contribute into the project.

## License

You can use it in your free open-source and commercial projects with a link to this repository.
