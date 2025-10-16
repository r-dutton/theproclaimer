[web] GET /api/accounting/reports/notes/policies/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.GetAll)  [L38–L42] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetPoliciesQuery [L41]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetPoliciesQueryHandler.Handle [L70–L227]
      └─ maps_to PolicyVariantForReportingSuiteLightDto [L199]
      └─ uses_service IControlledRepository<Policy>
        └─ method ReadQuery [L149]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<PolicyVariant>
        └─ method ReadQuery [L213]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ReportTemplate>
        └─ method ReadQuery [L153]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L175]
          └─ ... (no implementation details available)

