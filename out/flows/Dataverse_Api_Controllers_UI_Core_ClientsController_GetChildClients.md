[web] GET /api/ui/clients/{id}/children  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetChildClients)  [L147–L159] [auth=Authentication.UserPolicy]
  └─ maps_to ClientSearchLightDto [L150]
    └─ automapper.registration DataverseMappingProfile (Client->ClientSearchLightDto) [L181]
  └─ calls ClientRepository.ReadQuery [L150]
  └─ queries Client [L150]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L152]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L150]
  └─ uses_service UserService
    └─ method GetUserId [L152]

