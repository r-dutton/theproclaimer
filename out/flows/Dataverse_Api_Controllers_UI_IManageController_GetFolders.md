[web] GET /api/ui/imanage/folders  (Dataverse.Api.Controllers.UI.IManageController.GetFolders)  [L252–L268] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L256]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L256]
  └─ query IntegrationSettings [L256]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L256]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetFoldersAsync [L262]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetFoldersAsync [L19-L225]

