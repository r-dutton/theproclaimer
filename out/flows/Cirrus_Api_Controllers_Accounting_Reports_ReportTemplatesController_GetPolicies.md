[web] GET /api/accounting/reports/templates/{id}/policies  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetPolicies)  [L104–L108] status=200 [auth=user]
  └─ sends_request GetDefaultPoliciesForExistingReportQuery -> GetDefaultPoliciesForExistingReportQueryHandler [L107]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDefaultPoliciesForExistingReportQueryHandler.Handle [L44–L105]
      └─ calls ReportContentRepository.LoadReadProperties [L72]
      └─ maps_to PolicySelectorModel [L74]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L87]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<ReportTemplate> (Scoped (inferred))
        └─ method ReadQuery [L64]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportTemplateRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetDefaultPoliciesForExistingReportQuery
    └─ handlers 1
      └─ GetDefaultPoliciesForExistingReportQueryHandler
    └─ mappings 1
      └─ PolicySelectorModel

