[web] GET /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/fields/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplateField)  [L117–L132] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTemplateFieldDto [L125]
    └─ automapper.registration DataverseMappingProfile (DocumentTemplateField->DocumentTemplateFieldDto) [L435]
  └─ calls DocumentTemplateFieldRepository.ReadQuery [L125]
  └─ query DocumentTemplateField [L125]
    └─ reads_from DocumentTemplateFields
  └─ uses_service IControlledRepository<DocumentTemplateField>
    └─ method ReadQuery [L125]
      └─ ... (no implementation details available)

