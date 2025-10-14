[web] GET /api/ui/imanage  (Dataverse.Api.Controllers.UI.IManageController.UploadFile)  [L314–L326] [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L316]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L316]
  └─ queries IntegrationSettings [L316]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L316]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method UploadFile [L322]

