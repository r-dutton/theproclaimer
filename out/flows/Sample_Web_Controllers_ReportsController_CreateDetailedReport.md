[web] POST /api/reports  (Sample.Web.Controllers.ReportsController.CreateDetailedReport)  [L56–L76]
  └─ maps_to CreateDetailedReportCommand (var command) [L70]
    └─ converts_to Report [L15]
    └─ generic_pipeline_behaviors 1
      └─ ValidationBehavior
    └─ handled_by Sample.App.Handlers.CreateDetailedReportHandler.Handle [L12–L34]
      └─ calls ReportRepository.CreateDetailedAsync [L29]
        └─ maps_to Report [L33]
          └─ automapper.registration ReportProfile (CreateDetailedReportCommand->Report) [L15]
          └─ converts_to ReportDto [L21]
          └─ converts_to ReportSummaryDto [L24]
        └─ uses_cache IMemoryCache
          └─ method Remove [write] [L61]
        └─ uses_cache IMemoryCache
          └─ method Remove [write] [L44]
      └─ maps_to ReportDto [L32]
      └─ uses_service IMapper
        └─ method Map [L32]
      └─ uses_service IMediator
        └─ method Publish [L31]
      └─ uses_service IReportPublisher (AddScoped)
        └─ method PublishAsync [L30]
      └─ publishes ReportPublisher (queue=reports.events, subject=reports.published) [L30]
        └─ produces_event ReportPublished [L12]
        └─ produces_event ReportPublished [L12]
      └─ publishes_notification ReportGeneratedNotification [L31]
        └─ handled_by Sample.App.Notifications.ReportGeneratedHandler.Handle [L7–L23]
          └─ calls ReportAuditRepository.RecordAsync [L20]
            └─ maps_to ReportAudit [L24]
              └─ automapper.registration ReportProfile (ReportGeneratedNotification->ReportAudit) [L26]
          └─ uses_service ILogger<ReportGeneratedHandler>
            └─ method LogInformation [L21]
  └─ casts_to CreateDetailedReportDto (var specialized) [L59] [as]
    └─ converts_to CreateDetailedReportCommand [L13]
  └─ uses_client ReportsClient [L72]
    └─ calls GetDetails (GET /api/v2/reports/{*}/details, base=https://localhost:6001, config=Downstream:Reports:BaseUrl, target=ReportsApi) [L30]
      └─ target_service ReportsApi
        └─ [web] GET /api/reports/{id:guid}  (Sample.ReportsApi.Endpoints.MapGet)  [L26–L26]
        └─ [web] GET /api/v2/reports/{id:guid}/details  (Sample.ReportsApi.Controllers.ReportsController.GetDetails)  [L10–L15]
    └─ calls MapGet (GET /api/reports/{*}, base=https://localhost:6001, config=Downstream:Reports:BaseUrl, target=ReportsApi) [L17]
      └─ target_service ReportsApi
        └─ [web] GET /api/reports/{id:guid}  (Sample.ReportsApi.Endpoints.MapGet)  [L26–L26]
        └─ [web] GET /api/v2/reports/{id:guid}/details  (Sample.ReportsApi.Controllers.ReportsController.GetDetails)  [L10–L15]
    └─ calls MapPost (POST /api/reports/search, base=https://localhost:6001, config=Downstream:Reports:BaseUrl, target=ReportsApi) [L22]
      └─ target_service ReportsApi
        └─ [web] POST /api/reports/search  (Sample.ReportsApi.Endpoints.MapPost)  [L32–L32]
  └─ uses_validator ReportDtoValidator (ReportDto) [L74]
  └─ uses_service IMapper
    └─ method Map [L70]
  └─ sends_request CreateDetailedReportCommand [L71]
    └─ generic_pipeline_behaviors 1
      └─ ValidationBehavior
    └─ handled_by Sample.App.Handlers.CreateDetailedReportHandler.Handle [L12–L34]
      └─ calls ReportRepository.CreateDetailedAsync [L29]
        └─ maps_to Report [L33]
          └─ automapper.registration ReportProfile (CreateDetailedReportCommand->Report) [L15]
          └─ converts_to ReportDto [L21]
          └─ converts_to ReportSummaryDto [L24]
        └─ uses_cache IMemoryCache
          └─ method Remove [write] [L61]
        └─ uses_cache IMemoryCache
          └─ method Remove [write] [L44]
      └─ maps_to ReportDto [L32]
      └─ uses_service IMapper
        └─ method Map [L32]
      └─ uses_service IMediator
        └─ method Publish [L31]
      └─ uses_service IReportPublisher (AddScoped)
        └─ method PublishAsync [L30]
      └─ publishes ReportPublisher (queue=reports.events, subject=reports.published) [L30]
        └─ produces_event ReportPublished [L12]
        └─ produces_event ReportPublished [L12]
      └─ publishes_notification ReportGeneratedNotification [L31]
        └─ handled_by Sample.App.Notifications.ReportGeneratedHandler.Handle [L7–L23]
          └─ calls ReportAuditRepository.RecordAsync [L20]
            └─ maps_to ReportAudit [L24]
              └─ automapper.registration ReportProfile (ReportGeneratedNotification->ReportAudit) [L26]
          └─ uses_service ILogger<ReportGeneratedHandler>
            └─ method LogInformation [L21]
  └─ returns ReportDto [L71]

