[web] PUT /api/super/smart-workpapers/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.SmartWorkpapersController.UpdateSubscription)  [L120–L141] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TenantLightDto [L134]
  └─ uses_client WorkpapersClient [L135]
  └─ calls TenantRepository.WriteTable [L127]
  └─ writes_to Tenant [L127]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L134]
  └─ uses_service TenantRepository
    └─ method WriteTable [L127]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L137]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore>
        └─ method ReadQuery [L79]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]

