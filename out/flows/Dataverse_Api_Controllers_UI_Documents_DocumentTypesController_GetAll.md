[web] GET /api/ui/documents/types  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.GetAll)  [L36–L48] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTypeDto [L41]
    └─ automapper.registration DataverseMappingProfile (DocumentType->DocumentTypeDto) [L419]
  └─ calls DocumentTypeRepository.ReadQuery [L41]
  └─ queries DocumentType [L41]
    └─ reads_from DocumentTypes
  └─ uses_service IControlledRepository<DocumentType>
    └─ method ReadQuery [L41]

