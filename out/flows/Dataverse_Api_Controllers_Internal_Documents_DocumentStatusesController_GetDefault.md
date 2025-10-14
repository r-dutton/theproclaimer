[web] GET /api/internal/document-statuses/default  (Dataverse.Api.Controllers.Internal.Documents.DocumentStatusesController.GetDefault)  [L32–L42] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DocumentStatusDto [L35]
    └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L414]
  └─ calls DocumentStatusRepository.ReadQuery [L35]
  └─ queries DocumentStatus [L35]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method ReadQuery [L35]

