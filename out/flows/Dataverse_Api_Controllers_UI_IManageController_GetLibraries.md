[web] GET /api/ui/imanage/libraries  (Dataverse.Api.Controllers.UI.IManageController.GetLibraries)  [L144–L164] [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L147]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L147]
  └─ queries IntegrationSettings [L147]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L147]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetLibraries [L153]

