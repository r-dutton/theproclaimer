[web] GET /api/accounting/reports/styles/css/whitelist  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetWhiteListCssProperties)  [L73–L79] [auth=Authentication.AdminPolicy]
  └─ sends_request GetWhiteListCssPropertiesQuery [L77]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetWhiteListCssPropertiesQueryHandler.Handle [L12–L19]

