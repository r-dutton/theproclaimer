[web] GET /api/accounting/reports/notes/disclosure-templates/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.GetAll)  [L38–L42] [auth=Authentication.UserPolicy]
  └─ sends_request GetDisclosureTemplatesQuery [L41]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDisclosureTemplatesQueryHandler.Handle [L64–L215]
      └─ maps_to DisclosureVariantForReportingSuiteLightDto [L187]
      └─ uses_service IControlledRepository<DisclosureTemplate>
        └─ method ReadQuery [L143]
      └─ uses_service IControlledRepository<DisclosureVariant>
        └─ method ReadQuery [L201]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L163]

