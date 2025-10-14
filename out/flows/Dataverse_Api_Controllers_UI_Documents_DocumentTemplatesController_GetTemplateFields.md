[web] GET /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/fields  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplateFields)  [L94–L109] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTemplateFieldLightDto [L101]
    └─ automapper.registration DataverseMappingProfile (DocumentTemplateField->DocumentTemplateFieldLightDto) [L433]
  └─ calls DocumentTemplateFieldRepository.ReadQuery [L101]
  └─ queries DocumentTemplateField [L101]
    └─ reads_from DocumentTemplateFields
  └─ uses_service IControlledRepository<DocumentTemplateField>
    └─ method ReadQuery [L101]

