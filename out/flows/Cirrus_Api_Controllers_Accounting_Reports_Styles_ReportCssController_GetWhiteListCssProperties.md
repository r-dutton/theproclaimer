[web] GET /api/accounting/reports/styles/css/whitelist  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetWhiteListCssProperties)  [L73–L79] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request GetWhiteListCssPropertiesQuery -> GetWhiteListCssPropertiesQueryHandler [L77]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetWhiteListCssPropertiesQueryHandler.Handle [L12–L19]
  └─ impact_summary
    └─ requests 1
      └─ GetWhiteListCssPropertiesQuery
    └─ handlers 1
      └─ GetWhiteListCssPropertiesQueryHandler

