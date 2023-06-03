using CombatDicesTeam.Graphs.Generation.TemplateBased;

using FluentAssertions;

using Moq;

namespace CombatDicesTeam.Graphs.Generation.Tests;

public class TemplateBasedGraphGeneratorTests
{
    [Test]
    public void Create_MergeWays_CreatesMergeGraph()
    {
        // ARRANGE

        var wayGraph = new DirectedGraph<GraphWay<object>>();

        var way1TemplateMock = new Mock<IGraphTemplate<object>>();
        var way1Node = Mock.Of<IGraphNode<object>>();
        way1TemplateMock.Setup(x => x.Create(It.IsAny<IGraphTemplateContext<object>>()))
            .Returns(way1Node);
        var way1 = new GraphWay<object>(new[]
        {
            way1TemplateMock.Object
        });
        var way1WayGraphNode = new GraphNode<GraphWay<object>>(way1);
        wayGraph.AddNode(way1WayGraphNode);

        var way2TemplateMock = new Mock<IGraphTemplate<object>>();
        var way2Node = Mock.Of<IGraphNode<object>>();
        way2TemplateMock.Setup(x => x.Create(It.IsAny<IGraphTemplateContext<object>>()))
            .Returns(way2Node);
        var way2 = new GraphWay<object>(new[]
        {
            way2TemplateMock.Object
        });
        var way2WayGraphNode = new GraphNode<GraphWay<object>>(way2);
        wayGraph.AddNode(way2WayGraphNode);

        var terminalTemplateMock = new Mock<IGraphTemplate<object>>();
        var terminalNode = Mock.Of<IGraphNode<object>>();
        terminalTemplateMock.Setup(x => x.Create(It.IsAny<IGraphTemplateContext<object>>()))
            .Returns(terminalNode);

        var terminal = new GraphWay<object>(new[]
        {
            terminalTemplateMock.Object
        });
        var terminalWayGraphNode = new GraphNode<GraphWay<object>>(terminal);
        wayGraph.AddNode(terminalWayGraphNode);
        wayGraph.ConnectNodes(way1WayGraphNode, terminalWayGraphNode);
        wayGraph.ConnectNodes(way2WayGraphNode, terminalWayGraphNode);

        var generationConfig = new TemplateConfig<object>(wayGraph);

        var generator = new TemplateBasedGraphGenerator<object>(generationConfig);

        // ACT

        var graph = generator.Create();

        // ASSERT

        graph.GetAllNodes().Should().BeEquivalentTo(new[] { way1Node, way2Node, terminalNode });
        graph.GetNext(way1Node).Should().BeEquivalentTo(new[] { terminalNode });
        graph.GetNext(way2Node).Should().BeEquivalentTo(new[] { terminalNode });
    }

    [Test]
    public void Create_SingleWayGraph_CreatesGraphWithConnectedTwoNodes()
    {
        // ARRANGE

        var wayTemplateMock = new Mock<IGraphTemplate<object>>();
        var wayNode = Mock.Of<IGraphNode<object>>();
        wayTemplateMock.Setup(x => x.Create(It.IsAny<IGraphTemplateContext<object>>()))
            .Returns(wayNode);

        var terminalTemplateMock = new Mock<IGraphTemplate<object>>();
        var terminalNode = Mock.Of<IGraphNode<object>>();
        terminalTemplateMock.Setup(x => x.Create(It.IsAny<IGraphTemplateContext<object>>()))
            .Returns(terminalNode);

        var wayGraph = new DirectedGraph<GraphWay<object>>();

        var way = new GraphWay<object>(new[]
        {
            wayTemplateMock.Object,
            terminalTemplateMock.Object
        });

        wayGraph.AddNode(new GraphNode<GraphWay<object>>(way));

        var generationConfig = new TemplateConfig<object>(wayGraph);

        var generator = new TemplateBasedGraphGenerator<object>(generationConfig);

        // ACT

        var graph = generator.Create();

        // ASSERT

        graph.GetAllNodes().Should().BeEquivalentTo(new[] { wayNode, terminalNode });
        graph.GetNext(wayNode).Should().BeEquivalentTo(new[] { terminalNode });
    }
}