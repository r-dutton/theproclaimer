[web] GET /api/ui/documents/{documentId:guid}/versions/  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.GetById)  [L58–L65] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentVersionDto [L62]
    └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionDto) [L424]
  └─ calls DocumentVersionRepository.ReadQuery [L62]
  └─ query DocumentVersion [L62]
    └─ reads_from DocumentVersions
  └─ uses_service IControlledRepository<DocumentVersion>
    └─ method ReadQuery [L62]
      └─ ... (no implementation details available)

