[web] GET /core/v1/offices/detailed  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.FullQuery)  [L86–L91] status=200
  └─ calls OfficeRepository.ReadQuery [L89]
  └─ query Office [L89]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L89]
      └─ ... (no implementation details available)

