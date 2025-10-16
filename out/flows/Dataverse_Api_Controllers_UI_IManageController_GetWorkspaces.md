[web] GET /api/ui/imanage/workspaces  (Dataverse.Api.Controllers.UI.IManageController.GetWorkspaces)  [L183–L198] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L186]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L186]
  └─ query IntegrationSettings [L186]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L186]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetWorkspacesAsync [L192]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetWorkspacesAsync [L19-L225]

