[web] GET /api/export/binders/{binderId:guid}/trial-balance  (Workpapers.Next.API.Controllers.Workpapers.BinderExportController.GetTrialBalanceExportData)  [L39–L63] [auth=AuthorizationPolicies.User]
  └─ sends_request GetTrialBalanceExportDataQuery [L60]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetTrialBalanceExportDataQueryHandler.Handle [L32–L160]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L50]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L113]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L61]

