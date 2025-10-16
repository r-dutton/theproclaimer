[web] GET /core/v1/offices/  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.MinimalQuery)  [L69–L74] status=200
  └─ calls OfficeRepository.ReadQuery [L72]
  └─ query Office [L72]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L72]
      └─ ... (no implementation details available)

