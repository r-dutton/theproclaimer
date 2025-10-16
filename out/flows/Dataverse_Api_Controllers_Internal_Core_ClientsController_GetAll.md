[web] GET /api/internal/clients/audit  (Dataverse.Api.Controllers.Internal.Core.ClientsController.GetAll)  [L42–L48] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls ClientRepository.ReadQuery [L45]
  └─ query Client [L45]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)

