[web] GET /api/internal/offices/audit  (Dataverse.Api.Controllers.Internal.Core.OfficesController.GetAll)  [L41–L45] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls OfficeRepository.ReadQuery [L44]
  └─ query Office [L44]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L44]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository

