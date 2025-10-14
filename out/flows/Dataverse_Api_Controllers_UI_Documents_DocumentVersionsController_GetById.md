[web] GET /api/ui/documents/{documentId:guid}/versions/  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.GetById)  [L58–L65] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentVersionDto [L62]
    └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionDto) [L423]
  └─ calls DocumentVersionRepository.ReadQuery [L62]
  └─ queries DocumentVersion [L62]
    └─ reads_from DocumentVersions
  └─ uses_service IControlledRepository<DocumentVersion>
    └─ method ReadQuery [L62]

