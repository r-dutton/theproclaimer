[web] GET /api/import-runs/compare  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.GetComparison)  [L97–L105] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetCompareImportRunsQuery -> GetCompareImportRunsQueryHandler [L102]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetCompareImportRunsQueryHandler.Handle [L33–L165]
  └─ impact_summary
    └─ requests 1
      └─ GetCompareImportRunsQuery
    └─ handlers 1
      └─ GetCompareImportRunsQueryHandler

