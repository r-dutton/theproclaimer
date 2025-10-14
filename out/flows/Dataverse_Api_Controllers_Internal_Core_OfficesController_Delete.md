[web] DELETE /api/internal/offices/{id}  (Dataverse.Api.Controllers.Internal.Core.OfficesController.Delete)  [L88–L95] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls OfficeRepository.WriteQuery [L91]
  └─ writes_to Office [L91]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method WriteQuery [L91]

