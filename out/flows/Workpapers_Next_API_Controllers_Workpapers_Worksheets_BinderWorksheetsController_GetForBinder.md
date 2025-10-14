[web] GET /api/binders/{binderId:Guid}/worksheets/for-binder  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetForBinder)  [L107–L114] [auth=AuthorizationPolicies.User]
  └─ sends_request CanIAccessBinderQuery [L110]
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
  └─ sends_request GetWorkSheetsForBinderQuery [L111]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.GetWorkSheetsForBinderQueryHandler.Handle [L35–L98]
      └─ maps_to WorksheetDto [L65]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L56]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L68]
      └─ uses_service MatterCountQueryProcessor
        └─ method ProcessMatterCounts [L90]

