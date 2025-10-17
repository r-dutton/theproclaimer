[web] GET /api/accounting/reports/notes/disclosure-templates/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.GetAll)  [L38–L42] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetDisclosureTemplatesQuery -> GetDisclosureTemplatesQueryHandler [L41]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDisclosureTemplatesQueryHandler.Handle [L64–L215]
      └─ maps_to DisclosureVariantForReportingSuiteLightDto [L187]
      └─ uses_service IControlledRepository<DisclosureVariant> (Scoped (inferred))
        └─ method ReadQuery [L201]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.DisclosureVariantRepository.ReadQuery
      └─ uses_service IControlledRepository<DisclosureTemplate> (Scoped (inferred))
        └─ method ReadQuery [L143]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.DisclosureTemplateRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetDisclosureTemplatesQuery
    └─ handlers 1
      └─ GetDisclosureTemplatesQueryHandler
    └─ mappings 1
      └─ DisclosureVariantForReportingSuiteLightDto

