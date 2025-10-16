[web] GET /api/accounting/reports/styles/word/download/{reportStyleId:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportWordController.DownloadTemplate)  [L55–L79] status=200 [auth=Authentication.AdminPolicy]
  └─ calls ReportStyleRepository.ReadQuery [L59]
  └─ query ReportStyle [L59]
    └─ reads_from ReportStyles
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportStyle writes=0 reads=1

