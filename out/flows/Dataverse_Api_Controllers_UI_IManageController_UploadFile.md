[web] GET /api/ui/imanage  (Dataverse.Api.Controllers.UI.IManageController.UploadFile)  [L314–L326] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L316]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L316]
  └─ query IntegrationSettings [L316]
    └─ reads_from IntegrationSettingss
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method UploadFile [L322]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.UploadFile [L19-L225]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ IntegrationSettings writes=0 reads=1
    └─ services 1
      └─ IDatagetImanageService
    └─ mappings 1
      └─ IntegrationSettingsDto

