[web] POST /api/ui/trials/subscriptions/new  (Dataverse.Api.Controllers.UI.Trial.TrialSubscriptionsController.AddNewTenantTrialSubscriptions)  [L31–L36] [auth=Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateTrialSubscriptionsForNewTenantCommand [L35]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateTrialSubscriptionsForNewTenantCommandHandler.Handle [L33–L103]
      └─ calls TenantRepository.ReadTable [L99]
      └─ calls TenantRepository.ReadTable [L54]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L60]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L86]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L56]

