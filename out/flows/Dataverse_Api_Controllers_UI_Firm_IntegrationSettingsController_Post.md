[web] POST /api/ui/integration-settings/  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Post)  [L61–L74] status=201 [auth=Authentication.AdminPolicy]
  └─ calls IntegrationSettingsRepository.Add [L68]
  └─ calls IntegrationSettingsRepository.WriteQuery [L64]
  └─ insert IntegrationSettings [L68]
    └─ reads_from IntegrationSettingss
  └─ write IntegrationSettings [L64]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method WriteQuery [L64]
      └─ ... (no implementation details available)

