[web] GET /api/accounting/reports/published/template/{templateId:guid}  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List)  [L60–L77] [auth=user]
  └─ maps_to PublishedReportBatchDto [L72]
    └─ automapper.registration CirrusMappingProfile (PublishedReportBatch->PublishedReportBatchDto) [L601]
  └─ calls PublishedReportBatchRepository.ReadQuery [L66]
  └─ queries PublishedReportBatch [L66]
    └─ reads_from PublishedReportBatches
  └─ uses_service IControlledRepository<PublishedReportBatch>
    └─ method ReadQuery [L66]
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L70]
  └─ sends_request CanIAccessFileQuery [L70]
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

