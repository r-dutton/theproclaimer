[web] GET /documents/v1/documents/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.Get)  [L55–L60]
  └─ maps_to DocumentDto [L58]
    └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L404]
  └─ calls DocumentRepository.ReadQuery [L58]
  └─ queries Document [L58]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L58]

