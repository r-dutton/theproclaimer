[web] GET /api/ui/imanage/documents  (Dataverse.Api.Controllers.UI.IManageController.GetDocuments)  [L234–L250] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L238]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L238]
  └─ query IntegrationSettings [L238]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L238]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetDocumentsAsync [L244]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetDocumentsAsync [L19-L225]

