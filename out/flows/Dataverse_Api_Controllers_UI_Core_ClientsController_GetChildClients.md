[web] GET /api/ui/clients/{id}/children  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetChildClients)  [L147–L159] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ClientSearchLightDto [L150]
    └─ automapper.registration DataverseMappingProfile (Client->ClientSearchLightDto) [L182]
  └─ calls ClientRepository.ReadQuery [L150]
  └─ query Client [L150]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L152]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L150]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L152]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

