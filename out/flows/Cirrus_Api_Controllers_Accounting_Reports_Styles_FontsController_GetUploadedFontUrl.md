[web] GET /api/accounting/reports/styles/fonts/{fontName}/url  (Cirrus.Api.Controllers.Accounting.Reports.Styles.FontsController.GetUploadedFontUrl)  [L24–L30]
  └─ sends_request GetUploadedFontUrlQuery [L28]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetUploadedFontUrlQueryHandler.Handle [L23–L41]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L37]
      └─ uses_service UploadedFontsConfiguration
        └─ method UrlExpiryMin [L37]

