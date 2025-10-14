[web] POST /api/ui/trials/subscriptions  (Dataverse.Api.Controllers.UI.Trial.TrialSubscriptionsController.AddTrialSubscriptions)  [L24–L29] [auth=Authentication.AdminPolicy]
  └─ sends_request CreateTrialSubscriptionsCommand [L28]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateTrialSubscriptionCommandHandler.Handle [L44–L163]
      └─ maps_to UserUltraLightDto [L139]
      └─ uses_service IControlledRepository<User>
        └─ method WriteQuery [L156]
      └─ uses_service IMapper
        └─ method Map [L139]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L95]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L75]
      └─ uses_service TrialConfig
        └─ method UserLimit [L77]
      └─ uses_service UserService
        └─ method GetUserAsync [L138]
      └─ uses_cache IDistributedCache [L140]
        └─ method SetRecordAsync [write] [L140]
      └─ uses_cache IDistributedCache [L135]
        └─ method GetRecordAsync [read] [L135]
      └─ uses_cache IDistributedCache [L85]
        └─ method GetRecordAsync [read] [L85]

