[web] GET /api/ui/integration-settings/{apiType}  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Get)  [L48–L56] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to IntegrationSettingsDto [L51]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L51]
  └─ query IntegrationSettings [L51]
    └─ reads_from IntegrationSettingss
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ IntegrationSettings writes=0 reads=1
    └─ mappings 1
      └─ IntegrationSettingsDto

