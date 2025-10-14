[web] GET /api/ui/imanage/folders/{folderId}/subfolders  (Dataverse.Api.Controllers.UI.IManageController.GetSubFolders)  [L217–L232] [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L220]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L220]
  └─ queries IntegrationSettings [L220]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L220]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetSubFoldersAsync [L226]

