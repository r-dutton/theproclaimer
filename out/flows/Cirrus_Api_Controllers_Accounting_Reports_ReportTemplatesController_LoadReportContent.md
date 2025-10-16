[web] GET /api/accounting/reports/templates  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.LoadReportContent)  [L347–L356] status=200 [auth=user]
  └─ calls ReportContentRepository.LoadWriteProperties [L349]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L349]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadWriteProperties [L11-L65]
  └─ impact_summary
    └─ services 1
      └─ ReportContentRepository

