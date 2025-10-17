[web] GET /api/accounting/assets/reports/full-summary/download  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetFullSummaryReportExcel)  [L80–L86] status=200 [auth=user]
  └─ sends_request GetExportAssetReportQuery -> GetExportAssetReportQueryHandler [L84]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Assets.GetExportAssetReportQueryHandler.Handle [L69–L147]
      └─ uses_service IControlledRepository<DepreciationYear> (Scoped (inferred))
        └─ method ReadQuery [L100]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.DepreciationYearRepository.ReadQuery
      └─ uses_service IControlledRepository<ReportPageType> (Scoped (inferred))
        └─ method ReadQuery [L99]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportPageTypeRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L89]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ GetExportAssetReportQuery
    └─ handlers 1
      └─ GetExportAssetReportQueryHandler

