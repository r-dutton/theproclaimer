[web] GET /api/accounting/reports/notes/disclosure-templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Get)  [L44–L48] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetDisclosureTemplateQuery -> GetDisclosureTemplateQueryHandler [L47]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDisclosureTemplateQueryHandler.Handle [L30–L69]
      └─ maps_to DisclosureVariantForDisclosureTemplateDto [L54]
        └─ automapper.registration CirrusMappingProfile (DisclosureVariant->DisclosureVariantForDisclosureTemplateDto) [L850]
      └─ maps_to DisclosureTemplateDto [L47]
        └─ automapper.registration CirrusMappingProfile (DisclosureTemplate->DisclosureTemplateDto) [L826]
      └─ uses_service IControlledRepository<DisclosureVariant> (Scoped (inferred))
        └─ method ReadQuery [L54]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.DisclosureVariantRepository.ReadQuery
      └─ uses_service IControlledRepository<DisclosureTemplate> (Scoped (inferred))
        └─ method ReadQuery [L47]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.DisclosureTemplateRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetDisclosureTemplateQuery
    └─ handlers 1
      └─ GetDisclosureTemplateQueryHandler
    └─ mappings 2
      └─ DisclosureTemplateDto
      └─ DisclosureVariantForDisclosureTemplateDto

