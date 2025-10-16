[web] GET /api/accounting/reports/notes/disclosure-templates/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.GetAll)  [L38–L42] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetDisclosureTemplatesQuery [L41]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDisclosureTemplatesQueryHandler.Handle [L64–L215]
      └─ maps_to DisclosureVariantForReportingSuiteLightDto [L187]
      └─ uses_service IControlledRepository<DisclosureTemplate>
        └─ method ReadQuery [L143]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DisclosureVariant>
        └─ method ReadQuery [L201]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L163]
          └─ ... (no implementation details available)

