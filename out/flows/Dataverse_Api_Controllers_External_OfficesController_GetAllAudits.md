[web] GET /audit  (Dataverse.Api.Controllers.External.OfficesController.GetAllAudits)  [L39–L44]
  └─ calls OfficeRepository.ReadQuery [L43]
  └─ queries Office [L43]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L43]

