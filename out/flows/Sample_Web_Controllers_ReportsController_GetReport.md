[web] GET /api/reports/{id:guid}  (Sample.Web.Controllers.ReportsController.GetReport)  [L29–L35]
  └─ uses_validator ReportDtoValidator (ReportDto) [L33]
  └─ sends_request GetReportQuery [L32]
    └─ generic_pipeline_behaviors 1
      └─ ValidationBehavior
    └─ handled_by Sample.App.Handlers.GetReportHandler.Handle [L18–L82]
      └─ maps_to ReportDto [L59]
      └─ uses_service ILogger<GetReportHandler>
        └─ method LogInformation [L73]
      └─ uses_service IMapper
        └─ method Map [L59]
      └─ uses_service IOptions<ReportRetentionOptions>
        └─ method Value [L45]
      └─ uses_service IReportPublisher (AddScoped)
        └─ method PublishAsync [L65]
      └─ uses_cache IMemoryCache [L50]
        └─ method GetOrCreateAsync [read] [L50]
      └─ uses_options ReportRetentionOptions (Retention) [L47]
      └─ uses_options ReportRetentionOptions (Retention) [L45]
      └─ publishes ReportPublisher (queue=reports.events, subject=reports.published) [L65]
        └─ produces_event ReportPublished [L12]
        └─ produces_event ReportPublished [L12]
  └─ returns ReportDto [L32]

