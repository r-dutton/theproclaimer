[web] GET /documents/v1/documents/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.Get)  [L55–L60] status=200
  └─ maps_to DocumentDto [L58]
    └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L405]
  └─ calls DocumentRepository.ReadQuery [L58]
  └─ query Document [L58]
    └─ reads_from Documents
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Document writes=0 reads=1
    └─ mappings 1
      └─ DocumentDto

