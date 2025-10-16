[web] GET /api/accounting/reports/notes/disclosure-templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Get)  [L44–L48] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetDisclosureTemplateQuery [L47]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDisclosureTemplateQueryHandler.Handle [L30–L69]
      └─ maps_to DisclosureTemplateDto [L47]
        └─ automapper.registration CirrusMappingProfile (DisclosureTemplate->DisclosureTemplateDto) [L826]
      └─ maps_to DisclosureVariantForDisclosureTemplateDto [L54]
        └─ automapper.registration CirrusMappingProfile (DisclosureVariant->DisclosureVariantForDisclosureTemplateDto) [L850]
      └─ uses_service IControlledRepository<DisclosureTemplate>
        └─ method ReadQuery [L47]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DisclosureVariant>
        └─ method ReadQuery [L54]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L57]
          └─ ... (no implementation details available)

