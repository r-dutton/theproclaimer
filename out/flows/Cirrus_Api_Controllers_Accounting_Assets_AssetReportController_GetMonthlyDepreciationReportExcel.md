[web] GET /api/accounting/assets/reports/monthly/download  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetMonthlyDepreciationReportExcel)  [L142–L148] [auth=user]
  └─ sends_request GetExportAssetReportQuery [L146]
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

