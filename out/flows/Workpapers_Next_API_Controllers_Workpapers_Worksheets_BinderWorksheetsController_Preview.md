[web] GET /api/binders/{binderId:Guid}/worksheets/{id:Guid}/preview  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.Preview)  [L262–L270] [auth=AuthorizationPolicies.User]
  └─ sends_request CanIAccessBinderQuery [L265]
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
  └─ sends_request GetWorksheetPdfQuery [L267]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.GetWorksheetPdfQueryHandler.Handle [L37–L76]
      └─ maps_to WorksheetUltraLightDto [L63]
        └─ automapper.registration WorkpapersMappingProfile (Worksheet->WorksheetUltraLightDto) [L478]
      └─ uses_service IControlledRepository<Worksheet>
        └─ method ReadQuery [L63]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L54]

