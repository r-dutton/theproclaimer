[web] GET /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/fields  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplateFields)  [L100–L115] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTemplateFieldLightDto [L107]
    └─ automapper.registration DataverseMappingProfile (DocumentTemplateField->DocumentTemplateFieldLightDto) [L434]
  └─ calls DocumentTemplateFieldRepository.ReadQuery [L107]
  └─ query DocumentTemplateField [L107]
    └─ reads_from DocumentTemplateFields
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentTemplateField writes=0 reads=1
    └─ mappings 1
      └─ DocumentTemplateFieldLightDto

