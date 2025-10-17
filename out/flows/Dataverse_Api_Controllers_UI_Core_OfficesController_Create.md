[web] POST /api/ui/offices/  (Dataverse.Api.Controllers.UI.Core.OfficesController.Create)  [L212–L219] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls OfficeRepository.Add [L217]
  └─ insert Office [L217]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method Add [L217]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.Add [L8-L41]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L215]
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
      └─ Office writes=1 reads=0
    └─ clients 1
      └─ WorkpapersClient
    └─ services 2
      └─ FirmSettingsService
      └─ OfficeRepository
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

