[web] GET /api/accounting/assets/reports/full-summary/download  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetFullSummaryReportExcel)  [L80–L86] [auth=user]
  └─ sends_request GetExportAssetReportQuery [L84]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Assets.GetExportAssetReportQueryHandler.Handle [L69–L147]
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L100]
      └─ uses_service IControlledRepository<ReportPageType>
        └─ method ReadQuery [L99]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L89]

