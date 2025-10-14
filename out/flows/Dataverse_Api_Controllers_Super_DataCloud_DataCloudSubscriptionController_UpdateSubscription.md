[web] PUT /api/super/data-cloud/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.DataCloud.DataCloudSubscriptionController.UpdateSubscription)  [L76–L84] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateSubscriptionCommand [L83]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore>
        └─ method ReadQuery [L79]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]

