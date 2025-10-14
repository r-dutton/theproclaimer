[web] GET /api/accounting/reports/published/file/{publishedReportFileId:guid}/download  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.DownloadLink)  [L128–L157] [auth=user]
  └─ calls PublishedReportBatchRepository.ReadQuery [L147]
  └─ calls PublishedReportFileRepository.ReadQuery [L132]
  └─ queries PublishedReportBatch [L147]
    └─ reads_from PublishedReportBatches
  └─ queries PublishedReportFile [L132]
    └─ reads_from PublishedReportFiles
  └─ uses_service IControlledRepository<PublishedReportBatch>
    └─ method ReadQuery [L147]
  └─ uses_service IControlledRepository<PublishedReportFile>
    └─ method ReadQuery [L132]
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L133]
  └─ sends_request CanIAccessFileQuery [L133]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L71]
        └─ method DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache [L68]
        └─ method CreateAccessKey [write] [L68]
  └─ sends_request CreateDownloadCommand [L152]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
  └─ sends_request CreateDownloadCommand [L140]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]

