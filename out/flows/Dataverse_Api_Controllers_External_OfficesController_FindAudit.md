[web] GET /audit/find  (Dataverse.Api.Controllers.External.OfficesController.FindAudit)  [L46–L50] status=200
  └─ calls OfficeRepository.ReadQuery [L49]
  └─ query Office [L49]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L49]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository

