# Flow Coverage Checklist

- [x] Controllers: IOperation visitor (`ControllerOperationVisitor`) annotates mediator sends, notifications, mappings, and HTTP usage with interprocedural provenance.
- [x] CQRS Handlers: `CqrsOperationVisitor` captures repository, publish, mapper, HTTP flows with provenance/confidence edge props.
- [x] Messaging Publishers: `MessagingOperationVisitor` enriches publisher calls and feeds MessageLinker synthetic edges.
- [x] Notifications & Domain Events: dedicated visitors walk `Handle` methods interprocedurally to surface publishes, storage, mapping, and validator usage.
- [x] HTTP Clients: `HttpOperationVisitor` runs via FlowAnalysis to reconstruct routes and verbs with provenance metadata.
- [x] Entity Framework & Services: pipeline/service visitors gather storage/options/cache edges with deduplicated interprocedural analysis.
- [x] Edge Props: all emitted edges now include `provenance`/`confidence` props; MessageLinker stamps linker provenance.
- [x] Performance Guardrails: per-method FlowAnalysis caching and strict callsite predicates constrain interprocedural expansion.
