namespace GraphKit.Classification
{
    /// <summary>
    /// High-level semantic classification for calls encountered in flow analysis.
    /// </summary>
    public enum CallKind
    {
        MediatorSend,
        MediatorPublish,
        HandlerHandle,
        Repository,
        DbContext,
        Http,
        Mapper,
        Validator,
        Pipeline,
        DomainEventPublish,
        Cache,
        Logger,
        InfrastructureService,
        Other
    }
}
