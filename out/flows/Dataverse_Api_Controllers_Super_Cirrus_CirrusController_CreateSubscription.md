[web] POST /api/super/cirrus/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.CreateSubscription)  [L64–L102] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_client CirrusClient [L96]
  └─ uses_client CirrusClient [L81]
  └─ calls TenantRepository.WriteTable [L71]
  └─ writes_to Tenant [L71]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L71]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L99]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore>
        └─ method ReadQuery [L79]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]

