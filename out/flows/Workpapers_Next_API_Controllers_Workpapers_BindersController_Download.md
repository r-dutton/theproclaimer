[web] GET /api/binders/download-binder/{binderId}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Download)  [L176–L187] [auth=AuthorizationPolicies.User]
  └─ maps_to BinderUltraLightDto [L182]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderUltraLightDto) [L440]
  └─ calls BinderRepository.ReadQuery [L182]
  └─ queries Binder [L182]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L182]
  └─ sends_request CanIAccessBinderQuery [L180]
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

