[web] POST /api/accounting/reports/published/batch/{id:guid}/revert  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Revert)  [L103–L110] [auth=user]
  └─ calls PublishedReportBatchRepository.WriteQuery [L107]
  └─ writes_to PublishedReportBatch [L107]
    └─ reads_from PublishedReportBatches
  └─ uses_service IControlledRepository<PublishedReportBatch>
    └─ method WriteQuery [L107]
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L108]
  └─ sends_request CanIAccessFileQuery [L108]
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

