[web] PUT /api/ui/documents/types/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.Update)  [L68–L78] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentTypeRepository.WriteQuery [L71]
  └─ writes_to DocumentType [L71]
    └─ reads_from DocumentTypes
  └─ uses_service IControlledRepository<DocumentType>
    └─ method WriteQuery [L71]

