[web] GET /api/ui/documents/{documentId:guid}/versions/  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.GetById)  [L58–L65] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentVersionDto [L62]
    └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionDto) [L424]
  └─ calls DocumentVersionRepository.ReadQuery [L62]
  └─ query DocumentVersion [L62]
    └─ reads_from DocumentVersions
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentVersion writes=0 reads=1
    └─ mappings 1
      └─ DocumentVersionDto

