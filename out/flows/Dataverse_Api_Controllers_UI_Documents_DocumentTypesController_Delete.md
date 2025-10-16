[web] DELETE /api/ui/documents/types/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.Delete)  [L80–L90] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentTypeRepository.Remove [L87]
  └─ calls DocumentTypeRepository.WriteQuery [L83]
  └─ delete DocumentType [L87]
    └─ reads_from DocumentTypes
  └─ write DocumentType [L83]
    └─ reads_from DocumentTypes
  └─ uses_service IControlledRepository<DocumentType>
    └─ method WriteQuery [L83]
      └─ ... (no implementation details available)

