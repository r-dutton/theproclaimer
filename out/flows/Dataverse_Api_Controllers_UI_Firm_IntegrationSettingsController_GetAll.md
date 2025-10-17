[web] GET /api/ui/integration-settings/  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.GetAll)  [L37–L43] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to IntegrationSettingsDto [L40]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L40]
  └─ query IntegrationSettings [L40]
    └─ reads_from IntegrationSettingss
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ IntegrationSettings writes=0 reads=1
    └─ mappings 1
      └─ IntegrationSettingsDto

