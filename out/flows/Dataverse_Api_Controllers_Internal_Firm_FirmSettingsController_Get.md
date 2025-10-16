[web] GET /api/internal/firm-settings/  (Dataverse.Api.Controllers.Internal.Firm.FirmSettingsController.Get)  [L38–L60] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to FirmSettingsDto (var settings) [L53]
  └─ maps_to FirmSettingsDto [L48]
    └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L121]
  └─ maps_to FirmSettingsDto (var defaultSettings) [L44]
  └─ maps_to IntegrationSettingsDto [L55]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls FirmSettingsRepository.ReadQuery [L48]
  └─ calls IntegrationSettingsRepository.ReadQuery [L55]
  └─ query FirmSettings [L48]
    └─ reads_from FirmSettingss
  └─ query IntegrationSettings [L55]
    └─ reads_from IntegrationSettingss
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L42]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
  └─ uses_service IControlledRepository<FirmSettings>
    └─ method ReadQuery [L48]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L55]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L44]
      └─ ... (no implementation details available)

