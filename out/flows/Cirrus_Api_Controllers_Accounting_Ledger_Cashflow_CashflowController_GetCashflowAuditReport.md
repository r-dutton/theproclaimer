[web] GET /api/accounting/ledger/cashflow/audit-report  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowController.GetCashflowAuditReport)  [L29–L40] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetCashflowAuditQuery -> GetCashflowAuditQueryHandler [L38]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetCashflowAuditQueryHandler.Handle [L87–L305]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L250]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
      └─ uses_service GetAccountsQuery
        └─ method Execute [L247]
          └─ resolves_request Cirrus.Connections.DataGet.Queries.GetAccountsQuery.Execute
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
          └─ resolves_request DataGet.Connections.App.Queries.GetAccountsQuery.Execute
          └─ resolves_request DataGet.Connections.External.Bgl360.Api.Queries.GetAccountsQuery.Execute
          └─ +4 additional_requests elided
      └─ uses_service GetAccountTypesQuery
        └─ method Execute [L244]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowJournalLinesQuery
        └─ method Execute [L236]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowReconciliationLinesQuery
        └─ method Execute [L233]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowLinesQuery
        └─ method Execute [L226]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowCategoriesQuery
        └─ method Execute [L225]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L213]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L122]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 3
      └─ GetAccountsQuery
      └─ GetCashflowAuditQuery
      └─ GetTrialBalanceForDatesQuery
    └─ handlers 3
      └─ GetAccountsQueryHandler
      └─ GetCashflowAuditQueryHandler
      └─ GetTrialBalanceForDatesQueryHandler

