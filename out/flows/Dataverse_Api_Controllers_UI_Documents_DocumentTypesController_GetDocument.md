[web] GET /api/ui/documents/types/{id}  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.GetDocument)  [L50–L56] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTypeDto [L53]
    └─ automapper.registration DataverseMappingProfile (DocumentType->DocumentTypeDto) [L420]
  └─ calls DocumentTypeRepository.ReadQuery [L53]
  └─ query DocumentType [L53]
    └─ reads_from DocumentTypes
  └─ uses_service IControlledRepository<DocumentType>
    └─ method ReadQuery [L53]
      └─ ... (no implementation details available)

