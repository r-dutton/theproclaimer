[web] POST /api/ui/trials/subscriptions/new  (Dataverse.Api.Controllers.UI.Trial.TrialSubscriptionsController.AddNewTenantTrialSubscriptions)  [L31–L36] status=201 [auth=Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateTrialSubscriptionsForNewTenantCommand -> CreateTrialSubscriptionsForNewTenantCommandHandler [L35]
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateTrialSubscriptionsForNewTenantCommandHandler.Handle [L33–L103]
      └─ calls TenantRepository.ReadTable [L99]
      └─ calls OfficeRepository.ReadQuery [L60]
      └─ calls TenantRepository.ReadTable [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L86]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L56]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ requests 1
      └─ CreateTrialSubscriptionsForNewTenantCommand
    └─ handlers 1
      └─ CreateTrialSubscriptionsForNewTenantCommandHandler
    └─ caches 1
      └─ IMemoryCache

