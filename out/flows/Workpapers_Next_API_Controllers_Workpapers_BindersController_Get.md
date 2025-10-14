[web] GET /api/binders/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Get)  [L137–L174] [auth=AuthorizationPolicies.User]
  └─ maps_to BinderDto [L151]
    └─ automapper.registration ExternalApiMappingProfile (Binder->BinderDto) [L58]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderDto) [L450]
  └─ maps_to BinderUltraLightDto [L145]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderUltraLightDto) [L440]
  └─ calls BinderRepository.ReadQuery [L145]
  └─ queries Binder [L145]
    └─ reads_from Binders
  └─ uses_service ICirrusQueryService (AddScoped)
    └─ method GetDatasetsForFile [L159]
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L145]
  └─ sends_request CanIAccessBinderQuery [L141]
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

