[web] PUT /api/ui/offices/{id}  (Dataverse.Api.Controllers.UI.Core.OfficesController.Update)  [L224–L229] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request UpdateOfficeWithUsersCommand -> UpdateOfficeWithUsersCommandHandler [L227]
    └─ handled_by Dataverse.ApplicationService.Commands.Offices.UpdateOfficeWithUsersCommandHandler.Handle [L30–L127]
      └─ calls UserRepository.ReadQuery [L62]
      └─ calls OfficeRepository.WriteQuery [L52]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L51]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
            └─ uses_client WorkpapersClient [L78]
            └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
              └─ method GetCurrentSettingsAsync [L52]
                └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.GetCurrentSettingsAsync
            └─ uses_service TenantService
              └─ method GetCurrentSettingsAsync [L44]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentSettingsAsync [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
            └─ uses_cache IDistributedCache.GetRecordAsync [read] [L70]
            └─ uses_cache IDistributedCache.RemoveAsync [write] [L46]
  └─ impact_summary
    └─ clients 1
      └─ WorkpapersClient
    └─ requests 1
      └─ UpdateOfficeWithUsersCommand
    └─ handlers 1
      └─ UpdateOfficeWithUsersCommandHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

