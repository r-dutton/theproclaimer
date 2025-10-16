[web] DELETE /api/accounting/reports/saved-reports/{id}  (Cirrus.Api.Controllers.Accounting.Reports.SavedReportFilesController.Delete)  [L94–L104] status=200 [auth=user]
  └─ calls PublishedReportFileRepository.WriteQuery [L97]
  └─ write PublishedReportFile [L97]
    └─ reads_from PublishedReportFiles
  └─ uses_service IControlledRepository<PublishedReportFile>
    └─ method WriteQuery [L97]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L101]
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

