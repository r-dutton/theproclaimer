[web] GET /api/accounting/reports/published/batch/{id:guid}  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Detail)  [L94–L100] [auth=user]
  └─ maps_to PublishedReportBatchDto [L99]
  └─ calls PublishedReportBatchRepository.ReadQuery [L97]
  └─ queries PublishedReportBatch [L97]
    └─ reads_from PublishedReportBatches
  └─ uses_service IControlledRepository<PublishedReportBatch>
    └─ method ReadQuery [L97]
  └─ uses_service IMapper
    └─ method Map [L99]
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L98]
  └─ sends_request CanIAccessFileQuery [L98]
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

