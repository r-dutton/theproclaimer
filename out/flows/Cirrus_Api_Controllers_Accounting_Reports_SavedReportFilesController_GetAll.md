[web] GET /api/accounting/reports/saved-reports/forfile/{fileId}  (Cirrus.Api.Controllers.Accounting.Reports.SavedReportFilesController.GetAll)  [L82–L92] [auth=user]
  └─ maps_to SavedReportFileDto [L86]
    └─ automapper.registration CirrusMappingProfile (PublishedReportFile->SavedReportFileDto) [L594]
  └─ calls PublishedReportFileRepository.ReadQuery [L86]
  └─ queries PublishedReportFile [L86]
    └─ reads_from PublishedReportFiles
  └─ uses_service IControlledRepository<PublishedReportFile>
    └─ method ReadQuery [L86]
  └─ sends_request CanIAccessFileQuery [L85]
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

