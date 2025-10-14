[web] GET /api/ui/imanage/workspaces  (Dataverse.Api.Controllers.UI.IManageController.GetWorkspaces)  [L183–L198] [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L186]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L186]
  └─ queries IntegrationSettings [L186]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L186]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetWorkspacesAsync [L192]

