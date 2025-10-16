[web] GET /api/ui/documents/statuses/list  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetAllLight)  [L44–L53] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentStatusLightDto [L47]
    └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusLightDto) [L413]
  └─ calls DocumentStatusRepository.ReadQuery [L47]
  └─ query DocumentStatus [L47]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method ReadQuery [L47]
      └─ ... (no implementation details available)

