[web] PUT /api/ui/documents/types/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.Update)  [L68–L78] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentTypeRepository.WriteQuery [L71]
  └─ write DocumentType [L71]
    └─ reads_from DocumentTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DocumentType writes=1 reads=0

