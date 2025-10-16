[web] GET /api/ui/imanage/folders  (Dataverse.Api.Controllers.UI.IManageController.GetFolders)  [L252–L268] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L256]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L256]
  └─ query IntegrationSettings [L256]
    └─ reads_from IntegrationSettingss
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetFoldersAsync [L262]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetFoldersAsync [L19-L225]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ IntegrationSettings writes=0 reads=1
    └─ services 1
      └─ IDatagetImanageService
    └─ mappings 1
      └─ IntegrationSettingsDto

