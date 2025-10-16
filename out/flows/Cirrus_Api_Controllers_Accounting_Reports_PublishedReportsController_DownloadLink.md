[web] GET /api/accounting/reports/published/file/{publishedReportFileId:guid}/download  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.DownloadLink)  [L128–L157] status=200 [auth=user]
  └─ calls PublishedReportBatchRepository.ReadQuery [L147]
  └─ calls PublishedReportFileRepository.ReadQuery [L132]
  └─ query PublishedReportBatch [L147]
    └─ reads_from PublishedReportBatches
  └─ query PublishedReportFile [L132]
    └─ reads_from PublishedReportFiles
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L133]
      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
        └─ constructs RequestProcessorWrapper<CanIAccessFileQuery,Unit>
        └─ resolves IPipelineBehavior<CanIAccessFileQuery,Unit> chain
        └─ invokes IAsyncRequestHandler<CanIAccessFileQuery,Unit>.Handle
        └─ dispatches CanIAccessFileQuery [L133]
          └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method ProcessAsync [L90]
                └─ ... (service recursion detected)
            └─ uses_service IUserService (InstancePerLifetimeScope)
              └─ method GetUserId [L68]
                └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
                  └─ uses_service User
                    └─ method GetUserId [L67]
                      └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
                  └─ uses_service Guid?
                    └─ method GetUserId [L64]
                      └─ ... (no implementation details available)
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
            └─ uses_service ITenantService (AddScoped)
              └─ method GetCurrentTenant [L68]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
            └─ uses_service IRequestInfoService (AddScoped)
              └─ method IsValidServiceAccountRequest [L66]
                └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
            └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
            └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
            └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]
  └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L133]
  └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L152]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation DataGet.Data.BlobStorage.StorageService.CreateDownloadUrl [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]
  └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L140]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ PublishedReportBatch writes=0 reads=1
      └─ PublishedReportFile writes=0 reads=1
    └─ services 1
      └─ IRequestProcessor
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ CreateDownloadCommand
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ CreateDownloadCommandHandler
    └─ caches 1
      └─ IMemoryCache

