[web] GET /core/v1/offices/audits  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.GetAuditsQuery)  [L98–L104] status=200
  └─ maps_to EntityAuditDto [L101]
  └─ calls OfficeRepository.ReadQuery [L101]
  └─ query Office [L101]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L101]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository
    └─ mappings 1
      └─ EntityAuditDto

