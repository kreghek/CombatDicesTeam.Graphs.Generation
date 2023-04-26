namespace CombatDicesTeam.Graphs.Generation.TemplateBased;

public sealed record TemplateConfig<TNodePayload>(IReadOnlyCollection<IGraphTemplate<TNodePayload>> WayTemplates, IGraphTemplate<TNodePayload> TerminalTemplate);