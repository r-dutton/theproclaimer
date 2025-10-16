[web] POST /api/accounting/reports/published/batch/{id:guid}/revert  (Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.Revert)  [L103–L110] status=201 [auth=user]
  └─ calls PublishedReportBatchRepository.WriteQuery [L107]
  └─ write PublishedReportBatch [L107]
    └─ reads_from PublishedReportBatches
  └─ uses_service IControlledRepository<PublishedReportBatch>
    └─ method WriteQuery [L107]
      └─ ... (no implementation details available)
  └─ uses_service IRequestProcessor (InstancePerDependency)
    └─ method ProcessAsync [L108]
      └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L108]
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

