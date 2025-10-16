[web] GET /api/ui/imanage/workspaces/{workspaceId}/folders  (Dataverse.Api.Controllers.UI.IManageController.GetWorkspaceFolders)  [L200–L215] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L203]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L203]
  └─ query IntegrationSettings [L203]
    └─ reads_from IntegrationSettingss
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetWorkspaceFoldersAsync [L209]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetWorkspaceFoldersAsync [L19-L225]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ IntegrationSettings writes=0 reads=1
    └─ services 1
      └─ IDatagetImanageService
    └─ mappings 1
      └─ IntegrationSettingsDto

