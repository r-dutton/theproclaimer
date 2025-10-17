[web] GET /api/accounting/reports/masters/templates  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetAllMasterTemplates)  [L125–L129] status=200 [auth=user,admin]
  └─ sends_request GetReportMasterTemplates -> GetReportMasterTemplatesHandler [L128]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportMasterTemplates.GetReportMasterTemplatesHandler.Handle [L23–L47]
  └─ impact_summary
    └─ requests 1
      └─ GetReportMasterTemplates
    └─ handlers 1
      └─ GetReportMasterTemplatesHandler

