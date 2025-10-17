[web] POST /api/ui/documents/types  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.Create)  [L58–L66] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to DocumentTypeDto [L65]
  └─ calls DocumentTypeRepository.Add [L63]
  └─ insert DocumentType [L63]
    └─ reads_from DocumentTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DocumentType writes=1 reads=0
    └─ mappings 1
      └─ DocumentTypeDto

