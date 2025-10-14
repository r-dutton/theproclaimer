[web] GET /api/binder-sections/for-binder/{binderId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderSectionsController.GetForBinder)  [L53–L66] [auth=AuthorizationPolicies.User]
  └─ maps_to BinderSectionDto [L58]
    └─ automapper.registration ExternalApiMappingProfile (BinderSection->BinderSectionDto) [L189]
    └─ automapper.registration WorkpapersMappingProfile (BinderSection->BinderSectionDto) [L465]
  └─ calls BinderSectionRepository.ReadQuery [L58]
  └─ queries BinderSection [L58]
    └─ reads_from BinderSections
  └─ uses_service IControlledRepository<BinderSection>
    └─ method ReadQuery [L58]
  └─ sends_request CanIAccessBinderQuery [L56]
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

