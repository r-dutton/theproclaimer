[web] GET /api/ui/documents/types  (Dataverse.Api.Controllers.UI.Documents.DocumentTypesController.GetAll)  [L36–L48] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTypeDto [L41]
    └─ automapper.registration DataverseMappingProfile (DocumentType->DocumentTypeDto) [L420]
  └─ calls DocumentTypeRepository.ReadQuery [L41]
  └─ query DocumentType [L41]
    └─ reads_from DocumentTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentType writes=0 reads=1
    └─ mappings 1
      └─ DocumentTypeDto

