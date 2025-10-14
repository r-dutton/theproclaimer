[web] GET /api/accounting/reports/styles/css/editable-defaults  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportCssController.GetEditableStyleDefaultsCss)  [L61–L71] [auth=Authentication.AdminPolicy]
  └─ sends_request GetDefaultPdfCssQuery [L65]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetPdfCssQueryHandler.Handle [L13–L38]
      └─ uses_service FileManagerService
        └─ method GetCurrentFolder [L23]

