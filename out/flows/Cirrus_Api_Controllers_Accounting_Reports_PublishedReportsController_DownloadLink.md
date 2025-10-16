[web] GET /api/accounting/reports/published/file/{publishedReportFileId:guid}/download  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.DownloadLink)  [L128–L157] status=200 [auth=user]
  └─ calls PublishedReportBatchRepository.ReadQuery [L147]
  └─ calls PublishedReportFileRepository.ReadQuery [L132]
  └─ query PublishedReportBatch [L147]
    └─ reads_from PublishedReportBatches
  └─ query PublishedReportFile [L132]
    └─ reads_from PublishedReportFiles
  └─ uses_service IControlledRepository<PublishedReportBatch>
    └─ method ReadQuery [L147]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<PublishedReportFile>
    └─ method ReadQuery [L132]
      └─ ... (no implementation details available)
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L133]
      └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L133]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]
  └─ sends_request CreateDownloadCommand [L152]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation IStorageService.CreateDownloadUrl [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]
  └─ sends_request CreateDownloadCommand [L140]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation IStorageService.CreateDownloadUrl [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]

