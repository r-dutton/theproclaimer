[web] POST /api/super/smart-workpapers/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.SmartWorkpapersController.CreateSubscription)  [L80–L118] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TenantLightDto [L111]
  └─ maps_to TenantLightDto [L101]
  └─ uses_client WorkpapersClient [L112]
  └─ uses_client WorkpapersClient [L102]
  └─ uses_client WorkpapersClient [L96]
  └─ calls TenantRepository.WriteTable [L87]
  └─ writes_to Tenant [L87]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L101]
  └─ uses_service TenantRepository
    └─ method WriteTable [L87]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L115]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore>
        └─ method ReadQuery [L79]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]

