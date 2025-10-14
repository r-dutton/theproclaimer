[web] GET /documents/v1/documents/  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.MinimalQuery)  [L87–L93]
  └─ maps_to DocumentMinimalDto [L90]
  └─ calls DocumentRepository.ReadQuery [L90]
  └─ queries Document [L90]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L90]

