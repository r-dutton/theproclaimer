[web] GET /api/accounting/reports/notes/policies/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.GetAll)  [L38–L42] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetPoliciesQuery -> GetPoliciesQueryHandler [L41]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetPoliciesQueryHandler.Handle [L70–L227]
      └─ maps_to PolicyVariantForReportingSuiteLightDto [L199]
      └─ uses_service IControlledRepository<PolicyVariant> (Scoped (inferred))
        └─ method ReadQuery [L213]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.PolicyVariantRepository.ReadQuery
      └─ uses_service IControlledRepository<ReportTemplate> (Scoped (inferred))
        └─ method ReadQuery [L153]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<Policy> (Scoped (inferred))
        └─ method ReadQuery [L149]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Notes.PolicyRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetPoliciesQuery
    └─ handlers 1
      └─ GetPoliciesQueryHandler
    └─ mappings 1
      └─ PolicyVariantForReportingSuiteLightDto

