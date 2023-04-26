using CombatDicesTeam.Graphs.Generation.TemplateBased;

using FluentAssertions;

using Moq;

namespace CombatDicesTeam.Graphs.Generation.Tests;

public class TemplateBasedGraphGeneratorTests
{
    [Test]
    public void Test1()
    {
        // ARRANGE

        var wayTemplateMock = new Mock<IGraphTemplate<object>>();
        var wayNode = Mock.Of<IGraphNode<object>>();
        wayTemplateMock.Setup(x => x.Create(Mock.Of<IGraphTemplateContext<object>>()))
            .Returns(wayNode);
        
        var terminalTemplateMock = new Mock<IGraphTemplate<object>>();
        var terminalNode = Mock.Of<IGraphNode<object>>();
        terminalTemplateMock.Setup(x => x.Create(Mock.Of<IGraphTemplateContext<object>>()))
            .Returns(terminalNode);
        
        var generationConfig = new TemplateConfig<object>(new[]
        {
            wayTemplateMock.Object
        }, terminalTemplateMock.Object);
        
        var generator = new TemplateBasedGraphGenerator<object>(generationConfig);
        
        // ACT

        var graph = generator.Create();
        
        // ASSERT

        graph.GetAllNodes().Should().BeEquivalentTo(new[]{ wayNode, terminalNode });
        graph.GetNext(wayNode).Should().BeEquivalentTo(new[] { terminalNode });
    }
}