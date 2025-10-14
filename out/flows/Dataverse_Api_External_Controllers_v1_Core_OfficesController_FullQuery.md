[web] GET /core/v1/offices/detailed  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.FullQuery)  [L86–L91]
  └─ calls OfficeRepository.ReadQuery [L89]
  └─ queries Office [L89]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L89]

