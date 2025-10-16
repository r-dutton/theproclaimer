[web] POST /api/accounting/reports/published/publish  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Publish)  [L51–L57] status=201 [auth=user]
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L55]
      └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
      └─ ... (no implementation details available)
  └─ sends_request PublishReportsCommand [L55]
    └─ handled_by Cirrus.ApplicationService.Accounting.Reports.Publishing.PublishReportsCommandHandler.Handle [L45–L151]
      └─ maps_to PublishedReportBatchDto [L138]
      └─ uses_service IControlledRepository<PublishedReportBatch>
        └─ method ReadQuery [L84]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ReportTemplate>
        └─ method ReadQuery [L90]
          └─ ... (no implementation details available)
      └─ uses_service ILogger<PublishReportsCommandHandler>
        └─ method LogInformation [L136]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L138]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L99]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method UploadFile [L126]
          └─ implementation IStorageService.UploadFile [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L101]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.UploadFile [L126]
      └─ logs ILogger<PublishReportsCommandHandler> [Information] [L136]

