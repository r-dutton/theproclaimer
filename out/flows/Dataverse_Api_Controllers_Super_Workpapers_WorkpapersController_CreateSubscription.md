[web] POST /api/super/workpapers/{tenantId:Guid}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.CreateSubscription)  [L75–L116] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TenantLightDto [L109]
  └─ maps_to TenantLightDto [L99]
  └─ uses_client WorkpapersClient [L110]
  └─ uses_client WorkpapersClient [L100]
  └─ uses_client WorkpapersClient [L94]
  └─ calls TenantRepository.WriteTable [L82]
  └─ writes_to Tenant [L82]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L99]
  └─ uses_service TenantRepository
    └─ method WriteTable [L82]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L113]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore>
        └─ method ReadQuery [L79]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]

