[web] GET /api/accounting/reports/styles/css/editable-defaults  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetEditableStyleDefaultsCss)  [L61–L71] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request GetDefaultPdfCssQuery -> GetPdfCssQueryHandler [L65]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetPdfCssQueryHandler.Handle [L13–L38]
      └─ uses_service FileManagerService
        └─ method GetCurrentFolder [L23]
          └─ implementation Cirrus.ApplicationService.Services.FileManager.FileManagerService.GetCurrentFolder [L8-L34]
  └─ impact_summary
    └─ requests 1
      └─ GetDefaultPdfCssQuery
    └─ handlers 1
      └─ GetPdfCssQueryHandler

