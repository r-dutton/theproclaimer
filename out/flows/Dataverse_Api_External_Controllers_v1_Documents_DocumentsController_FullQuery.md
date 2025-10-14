[web] GET /documents/v1/documents/detailed  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.FullQuery)  [L104–L110]
  └─ maps_to DocumentDto [L107]
    └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L404]
  └─ calls DocumentRepository.ReadQuery [L107]
  └─ queries Document [L107]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L107]

