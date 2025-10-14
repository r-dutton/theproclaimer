[web] GET /api/accounting/reports/notes/policies/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.GetAll)  [L38–L42] [auth=Authentication.UserPolicy]
  └─ sends_request GetPoliciesQuery [L41]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetPoliciesQueryHandler.Handle [L70–L227]
      └─ maps_to PolicyVariantForReportingSuiteLightDto [L199]
      └─ uses_service IControlledRepository<Policy>
        └─ method ReadQuery [L149]
      └─ uses_service IControlledRepository<PolicyVariant>
        └─ method ReadQuery [L213]
      └─ uses_service IControlledRepository<ReportTemplate>
        └─ method ReadQuery [L153]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L175]

