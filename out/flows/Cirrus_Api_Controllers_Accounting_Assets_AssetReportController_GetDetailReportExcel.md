[web] GET /api/accounting/assets/reports/pool/download  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetDetailReportExcel)  [L171–L177] [auth=user]
  └─ sends_request GetExportAssetReportQuery [L175]
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

