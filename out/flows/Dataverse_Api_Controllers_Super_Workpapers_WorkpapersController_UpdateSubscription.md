[web] PUT /api/super/workpapers/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.UpdateSubscription)  [L118–L143] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TenantLightDto [L135]
  └─ uses_client WorkpapersClient [L137]
  └─ calls TenantRepository.WriteTable [L125]
  └─ writes_to Tenant [L125]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L135]
  └─ uses_service TenantRepository
    └─ method WriteTable [L125]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L139]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore>
        └─ method ReadQuery [L79]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]

