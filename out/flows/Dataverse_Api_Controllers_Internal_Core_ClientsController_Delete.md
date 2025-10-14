[web] DELETE /api/internal/clients/{id}  (Dataverse.Api.Controllers.Internal.Core.ClientsController.Delete)  [L88–L95] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls ClientRepository.WriteQuery [L91]
  └─ writes_to Client [L91]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L91]

