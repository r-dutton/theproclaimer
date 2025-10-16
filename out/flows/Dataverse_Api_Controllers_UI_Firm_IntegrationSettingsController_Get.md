[web] GET /api/ui/integration-settings/{apiType}  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Get)  [L48–L56] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to IntegrationSettingsDto [L51]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L51]
  └─ query IntegrationSettings [L51]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L51]
      └─ ... (no implementation details available)

