[web] GET /api/ui/documents/templates/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplate)  [L83–L92] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTemplateDto [L87]
    └─ automapper.registration DataverseMappingProfile (DocumentTemplate->DocumentTemplateDto) [L431]
  └─ calls DocumentTemplateRepository.ReadQuery [L87]
  └─ queries DocumentTemplate [L87]
    └─ reads_from DocumentTemplates
  └─ uses_service IControlledRepository<DocumentTemplate>
    └─ method ReadQuery [L87]

