[web] GET /api/ui/imanage/documents  (Dataverse.Api.Controllers.UI.IManageController.GetDocuments)  [L234–L250] [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L238]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L238]
  └─ queries IntegrationSettings [L238]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L238]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetDocumentsAsync [L244]

