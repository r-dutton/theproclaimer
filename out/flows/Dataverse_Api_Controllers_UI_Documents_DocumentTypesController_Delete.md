[web] DELETE /api/ui/documents/types/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.Delete)  [L80–L90] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentTypeRepository (methods: Remove,WriteQuery) [L87]
  └─ delete DocumentType [L87]
    └─ reads_from DocumentTypes
  └─ write DocumentType [L83]
    └─ reads_from DocumentTypes
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ DocumentType writes=2 reads=0

