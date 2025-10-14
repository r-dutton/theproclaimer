[web] GET /api/internal/firm-settings/  (Dataverse.Api.Controllers.Internal.Firm.FirmSettingsController.Get)  [L38–L60] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to FirmSettingsDto (var settings) [L53]
  └─ maps_to FirmSettingsDto [L48]
    └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L120]
  └─ maps_to FirmSettingsDto (var defaultSettings) [L44]
  └─ maps_to IntegrationSettingsDto [L55]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls FirmSettingsRepository.ReadQuery [L48]
  └─ calls IntegrationSettingsRepository.ReadQuery [L55]
  └─ queries FirmSettings [L48]
    └─ reads_from FirmSettingss
  └─ queries IntegrationSettings [L55]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<FirmSettings>
    └─ method ReadQuery [L48]
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L55]
  └─ uses_service IMapper
    └─ method Map [L44]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L42]

