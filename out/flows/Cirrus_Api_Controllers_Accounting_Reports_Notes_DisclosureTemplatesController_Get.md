[web] GET /api/accounting/reports/notes/disclosure-templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Get)  [L44–L48] [auth=Authentication.UserPolicy]
  └─ sends_request GetDisclosureTemplateQuery [L47]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDisclosureTemplateQueryHandler.Handle [L30–L69]
      └─ maps_to DisclosureTemplateDto [L47]
        └─ automapper.registration CirrusMappingProfile (DisclosureTemplate->DisclosureTemplateDto) [L826]
      └─ maps_to DisclosureVariantForDisclosureTemplateDto [L54]
        └─ automapper.registration CirrusMappingProfile (DisclosureVariant->DisclosureVariantForDisclosureTemplateDto) [L850]
      └─ uses_service IControlledRepository<DisclosureTemplate>
        └─ method ReadQuery [L47]
      └─ uses_service IControlledRepository<DisclosureVariant>
        └─ method ReadQuery [L54]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L57]

