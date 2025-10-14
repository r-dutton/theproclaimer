[web] GET /api/ui/documents/documents/external/{externalId}  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetDocumentByExternalId)  [L431–L450] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentDto [L442]
    └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L404]
  └─ calls DocumentRepository.ReadQuery [L434]
  └─ queries Document [L434]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L434]

