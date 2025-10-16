[web] GET /documents/v1/documents/detailed  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.FullQuery)  [L104–L110] status=200
  └─ maps_to DocumentDto [L107]
    └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L405]
  └─ calls DocumentRepository.ReadQuery [L107]
  └─ query Document [L107]
    └─ reads_from Documents
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Document writes=0 reads=1
    └─ mappings 1
      └─ DocumentDto

