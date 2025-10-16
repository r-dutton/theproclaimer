[web] GET /api/ui/documents/templates/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.GetTemplate)  [L89–L98] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentTemplateDto [L93]
    └─ automapper.registration DataverseMappingProfile (DocumentTemplate->DocumentTemplateDto) [L432]
  └─ calls DocumentTemplateRepository.ReadQuery [L93]
  └─ query DocumentTemplate [L93]
    └─ reads_from DocumentTemplates
  └─ uses_service IControlledRepository<DocumentTemplate>
    └─ method ReadQuery [L93]
      └─ ... (no implementation details available)

