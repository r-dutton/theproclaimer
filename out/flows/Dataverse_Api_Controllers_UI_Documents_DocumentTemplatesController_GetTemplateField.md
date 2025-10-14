[web] GET /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/fields/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplateField)  [L111–L126] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTemplateFieldDto [L119]
    └─ automapper.registration DataverseMappingProfile (DocumentTemplateField->DocumentTemplateFieldDto) [L434]
  └─ calls DocumentTemplateFieldRepository.ReadQuery [L119]
  └─ queries DocumentTemplateField [L119]
    └─ reads_from DocumentTemplateFields
  └─ uses_service IControlledRepository<DocumentTemplateField>
    └─ method ReadQuery [L119]

