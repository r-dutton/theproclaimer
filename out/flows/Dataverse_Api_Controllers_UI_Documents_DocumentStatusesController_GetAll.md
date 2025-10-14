[web] GET /api/ui/documents/statuses  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetAll)  [L33–L42] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentStatusDto [L36]
    └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L414]
  └─ calls DocumentStatusRepository.ReadQuery [L36]
  └─ queries DocumentStatus [L36]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method ReadQuery [L36]

