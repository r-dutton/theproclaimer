[web] GET /api/ui/documents/statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetById)  [L67–L75] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentStatusDto [L70]
    └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L414]
  └─ calls DocumentStatusRepository.ReadQuery [L70]
  └─ queries DocumentStatus [L70]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method ReadQuery [L70]

