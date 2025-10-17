[web] GET /api/accounting/reports/templates/{id}/disclosures  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetDisclosures)  [L124–L128] status=200 [auth=user]
  └─ sends_request GetDefaultDisclosuresForExistingReportQuery -> GetDefaultDisclosuresForExistingReportQueryHandler [L127]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.Notes.GetDefaultDisclosuresForExistingReportQueryHandler.Handle [L42–L104]
      └─ calls ReportContentRepository.LoadReadProperties [L70]
      └─ maps_to DisclosureModel [L72]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L85]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<ReportTemplate> (Scoped (inferred))
        └─ method ReadQuery [L62]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportTemplateRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetDefaultDisclosuresForExistingReportQuery
    └─ handlers 1
      └─ GetDefaultDisclosuresForExistingReportQueryHandler
    └─ mappings 1
      └─ DisclosureModel

