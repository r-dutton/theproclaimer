[web] PUT /api/internal/clients/{id}  (Dataverse.Api.Controllers.Internal.Core.ClientsController.UpdateDetails)  [L78–L86] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateClientCommand [L82]
    └─ handled_by Dataverse.ApplicationService.Commands.Clients.CreateOrUpdateClientCommandHandler.Handle [L48–L209]
      └─ maps_to ClientDto [L207]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L81]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L179]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L95]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Contact>
        └─ method ReadQuery [L94]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L89]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L90]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L91]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L207]
          └─ ... (no implementation details available)

