[web] GET /core/v1/offices/audits  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.GetAuditsQuery)  [L98–L104]
  └─ maps_to EntityAuditDto [L101]
  └─ calls OfficeRepository.ReadQuery [L101]
  └─ queries Office [L101]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L101]

