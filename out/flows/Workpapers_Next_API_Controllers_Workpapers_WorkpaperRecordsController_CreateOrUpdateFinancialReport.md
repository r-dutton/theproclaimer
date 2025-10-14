[web] POST /api/workpaper-records/{id:guid}/create-or-update-financial-report  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.CreateOrUpdateFinancialReport)  [L450–L475] [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L453]
  └─ writes_to WorkpaperRecord [L453]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method WriteQuery [L453]
  └─ sends_request CreateOrUpdateFinancialReportCommand [L471]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.CreateOrUpdateFinancialReportCommandHandler.Handle [L42–L215]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L156]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetReportData [L82]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L106]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L94]
      └─ uses_service UserService
        └─ method GetUser [L118]
  └─ sends_request CreateOrUpdateFinancialReportCommand [L466]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.CreateOrUpdateFinancialReportCommandHandler.Handle [L42–L215]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L156]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetReportData [L82]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L106]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L94]
      └─ uses_service UserService
        └─ method GetUser [L118]
  └─ sends_request CanIAccessBinderQuery [L457]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
      └─ uses_service UserService
        └─ method GetUserId [L91]
      └─ uses_cache IDistributedCache [L121]
        └─ method SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache [L109]
        └─ method DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache [L92]
        └─ method CreateAccessKey [write] [L92]

