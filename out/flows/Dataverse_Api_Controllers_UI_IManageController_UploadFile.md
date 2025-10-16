[web] GET /api/ui/imanage  (Dataverse.Api.Controllers.UI.IManageController.UploadFile)  [L314–L326] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L316]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L316]
  └─ query IntegrationSettings [L316]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L316]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method UploadFile [L322]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.UploadFile [L19-L225]

