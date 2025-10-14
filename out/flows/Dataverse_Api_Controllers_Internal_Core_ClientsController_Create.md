[web] POST /api/internal/clients/  (Dataverse.Api.Controllers.Internal.Core.ClientsController.Create)  [L70–L76] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateClientCommand [L74]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Clients.CreateOrUpdateClientCommandHandler.Handle [L48–L209]
      └─ maps_to ClientDto [L207]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L81]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L95]
      └─ uses_service IControlledRepository<Contact>
        └─ method ReadQuery [L94]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L89]
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L90]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L91]
      └─ uses_service IMapper
        └─ method Map [L207]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L179]

