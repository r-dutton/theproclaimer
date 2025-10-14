[web] GET /api/ui/documents/{documentId:guid}/versions/all  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.GetAll)  [L43–L56] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentVersionDto [L47]
    └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionDto) [L423]
  └─ calls DocumentVersionRepository.ReadQuery [L47]
  └─ queries DocumentVersion [L47]
    └─ reads_from DocumentVersions
  └─ uses_service IControlledRepository<DocumentVersion>
    └─ method ReadQuery [L47]

