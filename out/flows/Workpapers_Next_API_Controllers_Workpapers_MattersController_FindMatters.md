[web] GET /api/matters/find  (Workpapers.Next.API.Controllers.Workpapers.MattersController.FindMatters)  [L53–L61] [auth=AuthorizationPolicies.User]
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
  └─ sends_request FindMattersQuery [L58]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.Matters.FindMattersQueryHandler.Handle [L49–L188]
      └─ maps_to MatterWithMessagesDto [L120]
        └─ automapper.registration ExternalApiMappingProfile (Matter->MatterWithMessagesDto) [L210]
        └─ automapper.registration WorkpapersMappingProfile (Matter->MatterWithMessagesDto) [L784]
      └─ maps_to MatterWithMessagesDto [L109]
        └─ automapper.registration ExternalApiMappingProfile (Matter->MatterWithMessagesDto) [L210]
        └─ automapper.registration WorkpapersMappingProfile (Matter->MatterWithMessagesDto) [L784]
      └─ maps_to MatterWithMessagesDto [L99]
      └─ uses_service IControlledRepository<Matter>
        └─ method ReadQuery [L67]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L99]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L157]

