[web] GET /core/v1/offices/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.GetAuditQuery)  [L112–L117] status=200
  └─ maps_to EntityAuditDto [L115]
  └─ calls OfficeRepository.ReadQuery [L115]
  └─ query Office [L115]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L115]
      └─ ... (no implementation details available)

