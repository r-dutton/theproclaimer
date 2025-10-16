[web] POST /api/ui/trials/subscriptions  (Dataverse.Api.Controllers.UI.Trial.TrialSubscriptionsController.AddTrialSubscriptions)  [L24–L29] status=201 [auth=Authentication.AdminPolicy]
  └─ sends_request CreateTrialSubscriptionsCommand [L28]
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateTrialSubscriptionCommandHandler.Handle [L44–L163]
      └─ maps_to UserUltraLightDto [L139]
      └─ uses_service UserService
        └─ method GetUserAsync [L138]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L75]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service IControlledRepository<User>
        └─ method WriteQuery [L156]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L139]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L95]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service TrialConfig
        └─ method UserLimit [L77]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L140]
      └─ uses_cache IDistributedCache.GetRecordAsync [read] [L135]
      └─ uses_cache IDistributedCache.GetRecordAsync [read] [L85]

