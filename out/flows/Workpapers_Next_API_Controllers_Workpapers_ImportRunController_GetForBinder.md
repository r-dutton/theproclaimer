[web] GET /api/import-runs/for-binder  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.GetForBinder)  [L70–L90] [auth=AuthorizationPolicies.User]
  └─ maps_to ImportRunLightDto [L82]
    └─ automapper.registration CirrusMappingProfile (ImportRun->ImportRunLightDto) [L513]
    └─ automapper.registration WorkpapersMappingProfile (ImportRun->ImportRunLightDto) [L816]
  └─ calls BinderRepository.ReadQuery [L73]
  └─ calls ImportRunRepository.ReadQuery [L82]
  └─ queries ImportRun [L82]
    └─ reads_from ImportRuns
  └─ queries Binder [L73]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L73]
  └─ uses_service IControlledRepository<ImportRun>
    └─ method ReadQuery [L82]
  └─ sends_request CanIAccessBinderQuery [L77]
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

