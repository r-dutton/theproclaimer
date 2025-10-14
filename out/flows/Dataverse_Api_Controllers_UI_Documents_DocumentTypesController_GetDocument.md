[web] GET /api/ui/documents/types/{id}  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.GetDocument)  [L50–L56] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTypeDto [L53]
    └─ automapper.registration DataverseMappingProfile (DocumentType->DocumentTypeDto) [L419]
  └─ calls DocumentTypeRepository.ReadQuery [L53]
  └─ queries DocumentType [L53]
    └─ reads_from DocumentTypes
  └─ uses_service IControlledRepository<DocumentType>
    └─ method ReadQuery [L53]

