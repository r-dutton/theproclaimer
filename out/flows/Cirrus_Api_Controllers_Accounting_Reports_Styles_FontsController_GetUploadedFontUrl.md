[web] GET /api/accounting/reports/styles/fonts/{fontName}/url  (Cirrus.Api.Controllers.Accounting.Reports.Styles.FontsController.GetUploadedFontUrl)  [L24–L30] status=200
  └─ sends_request GetUploadedFontUrlQuery -> GetUploadedFontUrlQueryHandler [L28]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.ReportStyles.GetUploadedFontUrlQueryHandler.Handle [L23–L41]
      └─ uses_service UploadedFontsConfiguration
        └─ method UrlExpiryMin [L37]
          └─ ... (no implementation details available)
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L37]
          └─ implementation DataGet.Data.BlobStorage.StorageService.CreateDownloadUrl [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_storage IStorageService.CreateDownloadUrl [L37]
  └─ impact_summary
    └─ requests 1
      └─ GetUploadedFontUrlQuery
    └─ handlers 1
      └─ GetUploadedFontUrlQueryHandler

