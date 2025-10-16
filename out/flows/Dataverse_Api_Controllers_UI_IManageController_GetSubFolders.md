[web] GET /api/ui/imanage/folders/{folderId}/subfolders  (Dataverse.Api.Controllers.UI.IManageController.GetSubFolders)  [L217–L232] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L220]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L220]
  └─ query IntegrationSettings [L220]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L220]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetSubFoldersAsync [L226]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetSubFoldersAsync [L19-L225]

