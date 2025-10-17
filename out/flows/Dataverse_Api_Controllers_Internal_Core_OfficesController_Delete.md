[web] DELETE /api/internal/offices/{id}  (Dataverse.Api.Controllers.Internal.Core.OfficesController.Delete)  [L88–L95] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls OfficeRepository.WriteQuery [L91]
  └─ write Office [L91]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method WriteQuery [L91]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.WriteQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Office writes=1 reads=0
    └─ services 1
      └─ OfficeRepository

