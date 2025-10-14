[web] GET /api/ui/imanage/folders  (Dataverse.Api.Controllers.UI.IManageController.GetFolders)  [L252–L268] [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L256]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L256]
  └─ queries IntegrationSettings [L256]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L256]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetFoldersAsync [L262]

