[web] DELETE /api/accounting/reports/saved-reports/{id}  (Cirrus.Api.Controllers.Accounting.Reports.SavedReportFilesController.Delete)  [L94–L104] [auth=user]
  └─ calls PublishedReportFileRepository.WriteQuery [L97]
  └─ writes_to PublishedReportFile [L97]
    └─ reads_from PublishedReportFiles
  └─ uses_service IControlledRepository<PublishedReportFile>
    └─ method WriteQuery [L97]
  └─ sends_request CanIAccessFileQuery [L101]
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

