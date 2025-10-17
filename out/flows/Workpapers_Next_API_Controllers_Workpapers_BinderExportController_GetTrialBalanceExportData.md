[web] GET /api/export/binders/{binderId:guid}/trial-balance  (Workpapers.Next.API.Controllers.Workpapers.BinderExportController.GetTrialBalanceExportData)  [L39–L63] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetTrialBalanceExportDataQuery -> GetTrialBalanceExportDataQueryHandler [L60]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetTrialBalanceExportDataQueryHandler.Handle [L32–L160]
      └─ calls SourceAccountRepository.ReadQuery [L141]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L61]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L50]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetTrialBalanceExportDataQuery
    └─ handlers 1
      └─ GetTrialBalanceExportDataQueryHandler

