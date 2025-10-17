[web] GET /audit  (Dataverse.Api.Controllers.External.OfficesController.GetAllAudits)  [L39–L44] status=200
  └─ calls OfficeRepository.ReadQuery [L43]
  └─ query Office [L43]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L43]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository

