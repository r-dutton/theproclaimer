[web] POST /api/ui/teams/  (Dataverse.Api.Controllers.UI.Core.TeamsController.Create)  [L110–L117] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamRepository.Add [L115]
  └─ insert Team [L115]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method Add [L115]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.Add [L8-L40]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L113]
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
    └─ entities 1 (writes=1, reads=0)
      └─ Team writes=1 reads=0
    └─ clients 1
      └─ WorkpapersClient
    └─ services 2
      └─ FirmSettingsService
      └─ TeamRepository
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

