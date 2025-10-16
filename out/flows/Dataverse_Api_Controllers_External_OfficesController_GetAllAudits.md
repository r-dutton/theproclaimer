[web] GET /audit  (Dataverse.Api.Controllers.External.OfficesController.GetAllAudits)  [L39–L44] status=200
  └─ calls OfficeRepository.ReadQuery [L43]
  └─ query Office [L43]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L43]
      └─ ... (no implementation details available)

