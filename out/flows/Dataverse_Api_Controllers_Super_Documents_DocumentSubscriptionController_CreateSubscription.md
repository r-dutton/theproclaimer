[web] POST /api/super/documents/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Documents.DocumentSubscriptionController.CreateSubscription)  [L57–L74] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TenantRepository.WriteTable [L64]
  └─ writes_to Tenant [L64]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L64]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L71]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore>
        └─ method ReadQuery [L79]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]

