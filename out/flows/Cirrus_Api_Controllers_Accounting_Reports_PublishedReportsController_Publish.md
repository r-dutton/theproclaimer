[web] POST /api/accounting/reports/published/publish  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Publish)  [L51–L57] status=201 [auth=user]
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L55]
      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
        └─ constructs RequestProcessorWrapper<PublishReportsCommand,Unit>
        └─ resolves IPipelineBehavior<PublishReportsCommand,Unit> chain
        └─ invokes IAsyncRequestHandler<PublishReportsCommand,Unit>.Handle
        └─ dispatches PublishReportsCommand [L55]
          └─ handled_by Cirrus.ApplicationService.Accounting.Reports.Publishing.PublishReportsCommandHandler.Handle [L45–L151]
            └─ maps_to PublishedReportBatchDto [L138]
            └─ uses_service IStorageService (InstancePerLifetimeScope)
              └─ method UploadFile [L126]
                └─ implementation DataGet.Data.BlobStorage.StorageService.UploadFile [L11-L116]
                  └─ uses_service RequestContextProvider
                    └─ method GetContainer [L89]
                      └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
            └─ uses_service IUserService (InstancePerLifetimeScope)
              └─ method GetUserId [L101]
                └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
                  └─ uses_service User
                    └─ method GetUserId [L67]
                      └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
                  └─ uses_service Guid?
                    └─ method GetUserId [L64]
                      └─ ... (no implementation details available)
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method ProcessAsync [L99]
                └─ ... (service recursion detected)
            └─ uses_service IControlledRepository<ReportTemplate> (Scoped (inferred))
              └─ method ReadQuery [L90]
                └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportTemplateRepository.ReadQuery
            └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
              └─ method ReadQuery [L84]
                └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.ReadQuery
            └─ uses_storage IStorageService.UploadFile [L126]
            └─ logs ILogger<PublishReportsCommandHandler> [Information] [L136]
  └─ dispatches PublishReportsCommand -> PublishReportsCommandHandler [L55]
  └─ impact_summary
    └─ services 1
      └─ IRequestProcessor
    └─ requests 1
      └─ PublishReportsCommand
    └─ handlers 1
      └─ PublishReportsCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ PublishedReportBatchDto

