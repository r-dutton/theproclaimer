[web] POST /api/ui/trials/subscriptions/new  (Dataverse.Api.Controllers.UI.Trial.TrialSubscriptionsController.AddNewTenantTrialSubscriptions)  [L31–L36] status=201 [auth=Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateTrialSubscriptionsForNewTenantCommand [L35]
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateTrialSubscriptionsForNewTenantCommandHandler.Handle [L33–L103]
      └─ calls TenantRepository.ReadTable [L99]
      └─ calls TenantRepository.ReadTable [L54]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L56]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L60]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L86]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

