[web] POST /api/accounting/reports/published/publish  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Publish)  [L51–L57] [auth=user]
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L55]
  └─ sends_request PublishReportsCommand [L55]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Reports.Publishing.PublishReportsCommandHandler.Handle [L45–L151]
      └─ maps_to PublishedReportBatchDto [L138]
      └─ uses_service IControlledRepository<PublishedReportBatch>
        └─ method ReadQuery [L84]
      └─ uses_service IControlledRepository<ReportTemplate>
        └─ method ReadQuery [L90]
      └─ uses_service ILogger<PublishReportsCommandHandler>
        └─ method LogInformation [L136]
      └─ uses_service IMapper
        └─ method Map [L138]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L99]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method UploadFile [L126]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L101]

