[web] GET /api/internal/offices/audit  (Dataverse.Api.Controllers.Internal.Core.OfficesController.GetAll)  [L41–L45] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls OfficeRepository.ReadQuery [L44]
  └─ queries Office [L44]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L44]

