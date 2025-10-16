[web] DELETE /api/internal/offices/{id}  (Dataverse.Api.Controllers.Internal.Core.OfficesController.Delete)  [L88–L95] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls OfficeRepository.WriteQuery [L91]
  └─ write Office [L91]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method WriteQuery [L91]
      └─ ... (no implementation details available)

