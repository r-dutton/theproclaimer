[web] GET /api/ui/imanage/workspaces/{workspaceId}/folders  (Dataverse.Api.Controllers.UI.IManageController.GetWorkspaceFolders)  [L200–L215] [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L203]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L203]
  └─ queries IntegrationSettings [L203]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L203]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetWorkspaceFoldersAsync [L209]

