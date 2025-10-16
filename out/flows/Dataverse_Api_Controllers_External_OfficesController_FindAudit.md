[web] GET /audit/find  (Dataverse.Api.Controllers.External.OfficesController.FindAudit)  [L46–L50] status=200
  └─ calls OfficeRepository.ReadQuery [L49]
  └─ query Office [L49]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L49]
      └─ ... (no implementation details available)

