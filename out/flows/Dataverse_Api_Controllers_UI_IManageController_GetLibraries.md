[web] GET /api/ui/imanage/libraries  (Dataverse.Api.Controllers.UI.IManageController.GetLibraries)  [L144–L164] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L147]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L147]
  └─ query IntegrationSettings [L147]
    └─ reads_from IntegrationSettingss
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetLibraries [L153]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetLibraries [L19-L225]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ IntegrationSettings writes=0 reads=1
    └─ services 1
      └─ IDatagetImanageService
    └─ mappings 1
      └─ IntegrationSettingsDto

