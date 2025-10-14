[web] DELETE /api/ui/documents/types/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.Delete)  [L80–L90] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentTypeRepository.Remove [L87]
  └─ calls DocumentTypeRepository.WriteQuery [L83]
  └─ writes_to DocumentType [L87]
    └─ reads_from DocumentTypes
  └─ writes_to DocumentType [L83]
    └─ reads_from DocumentTypes
  └─ uses_service IControlledRepository<DocumentType>
    └─ method WriteQuery [L83]

