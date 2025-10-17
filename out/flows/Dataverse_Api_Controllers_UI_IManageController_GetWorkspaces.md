[web] GET /api/ui/imanage/workspaces  (Dataverse.Api.Controllers.UI.IManageController.GetWorkspaces)  [L183–L198] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L186]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L186]
  └─ query IntegrationSettings [L186]
    └─ reads_from IntegrationSettingss
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetWorkspacesAsync [L192]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetWorkspacesAsync [L19-L225]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ IntegrationSettings writes=0 reads=1
    └─ services 1
      └─ IDatagetImanageService
    └─ mappings 1
      └─ IntegrationSettingsDto

