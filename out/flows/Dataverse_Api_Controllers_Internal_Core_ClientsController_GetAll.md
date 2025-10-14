[web] GET /api/internal/clients/audit  (Dataverse.Api.Controllers.Internal.Core.ClientsController.GetAll)  [L42–L48] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls ClientRepository.ReadQuery [L45]
  └─ queries Client [L45]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L45]

