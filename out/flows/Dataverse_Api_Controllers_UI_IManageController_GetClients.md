[web] GET /api/ui/imanage/clients  (Dataverse.Api.Controllers.UI.IManageController.GetClients)  [L166–L181] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L169]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L169]
  └─ query IntegrationSettings [L169]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L169]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetClientsAsync [L175]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetClientsAsync [L19-L225]

