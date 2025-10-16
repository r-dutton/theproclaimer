[web] GET /api/accounting/reports/page-layouts/new-template  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportPageLayoutController.GetNewTemplate)  [L78–L82] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetReportPageLayoutQuery -> GetReportPageLayoutsQueryHandler [L81]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportPageLayoutsQueryHandler.Handle [L47–L197]
      └─ maps_to ReportPageLayoutDto [L124]
      └─ uses_service IControlledRepository<ReportPageLayout> (Scoped (inferred))
        └─ method ReadQuery [L95]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.Layout.ReportPageLayoutRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L70]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ GetReportPageLayoutQuery
    └─ handlers 1
      └─ GetReportPageLayoutsQueryHandler
    └─ mappings 1
      └─ ReportPageLayoutDto

