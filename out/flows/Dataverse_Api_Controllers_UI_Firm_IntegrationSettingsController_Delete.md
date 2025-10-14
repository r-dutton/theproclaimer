[web] DELETE /api/ui/integration-settings/{id:guid}  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Delete)  [L89–L94] [auth=Authentication.AdminPolicy]
  └─ calls IntegrationSettingsRepository.Remove [L93]
  └─ calls IntegrationSettingsRepository.WriteQuery [L92]
  └─ writes_to IntegrationSettings [L93]
    └─ reads_from IntegrationSettingss
  └─ writes_to IntegrationSettings [L92]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method WriteQuery [L92]

