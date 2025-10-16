[web] PUT /api/ui/integration-settings/{id:guid}  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Put)  [L79–L84] status=200 [auth=Authentication.AdminPolicy]
  └─ calls IntegrationSettingsRepository.WriteQuery [L82]
  └─ write IntegrationSettings [L82]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method WriteQuery [L82]
      └─ ... (no implementation details available)

