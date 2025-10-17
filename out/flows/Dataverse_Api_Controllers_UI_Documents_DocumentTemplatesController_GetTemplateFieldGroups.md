[web] GET /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/field-groups  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplateFieldGroups)  [L134–L149] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTemplateFieldGroupDto [L141]
    └─ automapper.registration DataverseMappingProfile (DocumentTemplateFieldGroup->DocumentTemplateFieldGroupDto) [L436]
  └─ calls DocumentTemplateFieldGroupRepository.ReadQuery [L141]
  └─ query DocumentTemplateFieldGroup [L141]
    └─ reads_from DocumentTemplateFieldGroups
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentTemplateFieldGroup writes=0 reads=1
    └─ mappings 1
      └─ DocumentTemplateFieldGroupDto

