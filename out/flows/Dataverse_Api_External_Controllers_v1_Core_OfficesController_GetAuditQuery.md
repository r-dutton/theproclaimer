[web] GET /core/v1/offices/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.GetAuditQuery)  [L112–L117] status=200
  └─ maps_to EntityAuditDto [L115]
  └─ calls OfficeRepository.ReadQuery [L115]
  └─ query Office [L115]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L115]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository
    └─ mappings 1
      └─ EntityAuditDto

