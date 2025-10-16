[web] GET /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/field-groups  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplateFieldGroups)  [L134–L149] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTemplateFieldGroupDto [L141]
    └─ automapper.registration DataverseMappingProfile (DocumentTemplateFieldGroup->DocumentTemplateFieldGroupDto) [L436]
  └─ calls DocumentTemplateFieldGroupRepository.ReadQuery [L141]
  └─ query DocumentTemplateFieldGroup [L141]
    └─ reads_from DocumentTemplateFieldGroups
  └─ uses_service IControlledRepository<DocumentTemplateFieldGroup>
    └─ method ReadQuery [L141]
      └─ ... (no implementation details available)

