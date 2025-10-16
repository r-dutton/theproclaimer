[web] DELETE /api/internal/clients/{id}  (Dataverse.Api.Controllers.Internal.Core.ClientsController.Delete)  [L88–L95] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls ClientRepository.WriteQuery [L91]
  └─ write Client [L91]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L91]
      └─ ... (no implementation details available)

