namespace GraphKit.Constants
{
    public static class EdgeKinds
    {
        public const string Calls = "calls";
        public const string SendsRequest = "sends_request";
        public const string HandledBy = "handled_by";
        public const string ProcessedBy = "processed_by";
        public const string MapsTo = "maps_to";
        public const string UsesClient = "uses_client";
        public const string UsesService = "uses_service";
        public const string UsesStorage = "uses_storage";
        public const string UsesCache = "uses_cache";
        public const string UsesOptions = "uses_options";
        public const string UsesConfiguration = "uses_configuration";
        public const string ReadsFrom = "reads_from";
        public const string Queries = "queries";
        public const string Publishes = "publishes";
        public const string PublishesNotification = "publishes_notification";
        public const string PublishesDomainEvent = "publishes_domain_event";
        public const string RequestProcessorDispatch = "requestprocessor.dispatch";
        public const string GeneratedFrom = "generated_from";
        public const string Implements = "implemented_by";
        public const string InvokesDomain = "invokes_domain";
        public const string Logs = "logs";
        public const string Validates = "validates";
        public const string Validation = "validation";
        public const string ServiceLocated = "service_located";
        public const string Returns = "returns";
        public const string CastsTo = "casts_to";
        public const string ConvertsTo = "converts_to";
        public const string Manages = "manages";
    }
}
