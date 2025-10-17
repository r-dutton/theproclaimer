[web] GET /core/v1/offices/  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.MinimalQuery)  [L69–L74] status=200
  └─ calls OfficeRepository.ReadQuery [L72]
  └─ query Office [L72]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L72]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository

