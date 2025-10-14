[web] PUT /api/ui/integration-settings/{id:guid}  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Put)  [L79–L84] [auth=Authentication.AdminPolicy]
  └─ calls IntegrationSettingsRepository.WriteQuery [L82]
  └─ writes_to IntegrationSettings [L82]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method WriteQuery [L82]

