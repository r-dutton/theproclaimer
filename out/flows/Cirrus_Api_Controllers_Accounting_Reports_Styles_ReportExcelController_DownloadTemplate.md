[web] GET /api/accounting/reports/styles/excel/download/{reportStyleId:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportExcelController.DownloadTemplate)  [L55–L79] [auth=Authentication.AdminPolicy]
  └─ calls ReportStyleRepository.ReadQuery [L59]
  └─ queries ReportStyle [L59]
    └─ reads_from ReportStyles
  └─ uses_service IControlledRepository<ReportStyle>
    └─ method ReadQuery [L59]

