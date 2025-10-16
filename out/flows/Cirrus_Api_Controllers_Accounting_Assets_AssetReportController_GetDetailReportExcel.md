[web] GET /api/accounting/assets/reports/pool/download  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetDetailReportExcel)  [L171–L177] status=200 [auth=user]
  └─ sends_request GetExportAssetReportQuery [L175]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Assets.GetExportAssetReportQueryHandler.Handle [L69–L147]
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L100]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ReportPageType>
        └─ method ReadQuery [L99]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L89]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

