[web] POST /api/ui/documents/types  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.Create)  [L58–L66] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to DocumentTypeDto [L65]
  └─ calls DocumentTypeRepository.Add [L63]
  └─ writes_to DocumentType [L63]
    └─ reads_from DocumentTypes
  └─ uses_service IControlledRepository<DocumentType>
    └─ method Add [L63]
  └─ uses_service IMapper
    └─ method Map [L65]

