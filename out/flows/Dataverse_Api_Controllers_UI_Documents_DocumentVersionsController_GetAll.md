[web] GET /api/ui/documents/{documentId:guid}/versions/all  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.GetAll)  [L43–L56] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentVersionDto [L47]
    └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionDto) [L424]
  └─ calls DocumentVersionRepository.ReadQuery [L47]
  └─ query DocumentVersion [L47]
    └─ reads_from DocumentVersions
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentVersion writes=0 reads=1
    └─ mappings 1
      └─ DocumentVersionDto

