[web] GET /api/binders/{binderId:Guid}/worksheets/for-binder/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetWorksheetForBinder)  [L116–L124] [auth=AuthorizationPolicies.User]
  └─ maps_to WorksheetUltraLightDto [L120]
    └─ automapper.registration WorkpapersMappingProfile (Worksheet->WorksheetUltraLightDto) [L478]
  └─ calls WorksheetRepository.ReadQuery [L120]
  └─ queries Worksheet [L120]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L120]
  └─ sends_request CanIAccessBinderQuery [L119]
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

