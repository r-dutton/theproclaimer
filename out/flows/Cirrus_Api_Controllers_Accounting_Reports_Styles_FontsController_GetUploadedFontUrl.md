[web] GET /api/accounting/reports/styles/fonts/{fontName}/url  (Cirrus.Api.Controllers.Accounting.Reports.Styles.FontsController.GetUploadedFontUrl)  [L24–L30] status=200
  └─ sends_request GetUploadedFontUrlQuery [L28]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetUploadedFontUrlQueryHandler.Handle [L23–L41]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L37]
          └─ implementation IStorageService.CreateDownloadUrl [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service UploadedFontsConfiguration
        └─ method UrlExpiryMin [L37]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.CreateDownloadUrl [L37]

