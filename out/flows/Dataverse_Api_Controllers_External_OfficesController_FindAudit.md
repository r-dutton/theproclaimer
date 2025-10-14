[web] GET /audit/find  (Dataverse.Api.Controllers.External.OfficesController.FindAudit)  [L46–L50]
  └─ calls OfficeRepository.ReadQuery [L49]
  └─ queries Office [L49]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L49]

