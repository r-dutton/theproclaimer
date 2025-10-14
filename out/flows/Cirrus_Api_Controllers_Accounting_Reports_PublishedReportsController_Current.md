[web] GET /api/accounting/reports/published/template/{templateId:guid}/current  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Current)  [L80–L91] [auth=user]
  └─ maps_to PublishedReportBatchDto [L90]
  └─ calls PublishedReportBatchRepository.ReadQuery [L83]
  └─ queries PublishedReportBatch [L83]
    └─ reads_from PublishedReportBatches
  └─ uses_service IControlledRepository<PublishedReportBatch>
    └─ method ReadQuery [L83]
  └─ uses_service IMapper
    └─ method Map [L90]
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L88]
  └─ sends_request CanIAccessFileQuery [L88]
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

