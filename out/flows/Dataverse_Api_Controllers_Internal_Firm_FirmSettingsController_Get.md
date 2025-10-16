[web] GET /api/internal/firm-settings/  (Dataverse.Api.Controllers.Internal.Firm.FirmSettingsController.Get)  [L38–L60] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to IntegrationSettingsDto [L55]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ maps_to FirmSettingsDto (var settings) [L53]
  └─ maps_to FirmSettingsDto [L48]
    └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L121]
  └─ maps_to FirmSettingsDto (var defaultSettings) [L44]
  └─ calls IntegrationSettingsRepository.ReadQuery [L55]
  └─ calls FirmSettingsRepository.ReadQuery [L48]
  └─ query IntegrationSettings [L55]
    └─ reads_from IntegrationSettingss
  └─ query FirmSettings [L48]
    └─ reads_from FirmSettingss
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L42]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ FirmSettings writes=0 reads=1
      └─ IntegrationSettings writes=0 reads=1
    └─ services 1
      └─ TenantService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ FirmSettingsDto
      └─ IntegrationSettingsDto

