[web] GET /api/ui/documents/statuses/many  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetMany)  [L55–L65] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentStatusDto [L58]
    └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L415]
  └─ calls DocumentStatusRepository.ReadQuery [L58]
  └─ query DocumentStatus [L58]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method ReadQuery [L58]
      └─ ... (no implementation details available)

