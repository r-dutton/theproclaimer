[web] GET /api/accounting/reports/templates  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.LoadReportContent)  [L347–L356] [auth=user]
  └─ calls ReportContentRepository.LoadWriteProperties [L349]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L349]

