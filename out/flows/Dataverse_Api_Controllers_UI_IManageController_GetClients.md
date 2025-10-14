[web] GET /api/ui/imanage/clients  (Dataverse.Api.Controllers.UI.IManageController.GetClients)  [L166–L181] [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L169]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L169]
  └─ queries IntegrationSettings [L169]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L169]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetClientsAsync [L175]

