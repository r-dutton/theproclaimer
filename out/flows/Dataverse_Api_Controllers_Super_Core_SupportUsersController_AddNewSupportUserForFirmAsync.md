[web] POST /api/super/support-users  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.AddNewSupportUserForFirmAsync)  [L36–L41] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request AddSupportUserCommand -> AddSupportUserCommandHandler [L39]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.AddSupportUserCommandHandler.Handle [L34–L149]
      └─ calls UserRepository.ReadQuery [L77]
      └─ calls TenantRepository.ReadTable [L64]
      └─ uses_service UnitOfWork
        └─ method Table [L129]
          └─ implementation UnitOfWork.Table
      └─ uses_service TenantLicenseService
        └─ method GetFirmSubscriptions [L113]
          └─ implementation Dataverse.ApplicationService.Services.TenantLicenseService.GetFirmSubscriptions [L22-L185]
            └─ uses_service TenantService
              └─ method GetFirmSubscriptions [L74]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetFirmSubscriptions [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L62]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L61]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
  └─ impact_summary
    └─ requests 1
      └─ AddSupportUserCommand
    └─ handlers 1
      └─ AddSupportUserCommandHandler
    └─ caches 1
      └─ IMemoryCache

