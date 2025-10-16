[web] GET /api/accounting/reports/saved-reports/{id}  (Cirrus.Api.Controllers.Accounting.Reports.SavedReportFilesController.Get)  [L53–L64] status=200 [auth=user]
  └─ maps_to SavedReportFileDto [L63]
  └─ calls PublishedReportFileRepository.ReadQuery [L57]
  └─ query PublishedReportFile [L57]
    └─ reads_from PublishedReportFiles
  └─ uses_service IControlledRepository<PublishedReportFile>
    └─ method ReadQuery [L57]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L63]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L61]
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

