[web] GET /api/ui/integration-settings/  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.GetAll)  [L37–L43] [auth=Authentication.AdminPolicy]
  └─ maps_to IntegrationSettingsDto [L40]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L40]
  └─ queries IntegrationSettings [L40]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L40]

