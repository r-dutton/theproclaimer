[web] GET /api/accounting/reports/masters/templates  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetAllMasterTemplates)  [L125–L129] [auth=user,admin]
  └─ sends_request GetReportMasterTemplates [L128]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportMasterTemplates.GetReportMasterTemplatesHandler.Handle [L23–L47]

