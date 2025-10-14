[web] GET /api/accounting/ledger/cashflow/audit-report  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowController.GetCashflowAuditReport)  [L29–L40] [auth=Authentication.UserPolicy]
  └─ sends_request GetCashflowAuditQuery [L38]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetCashflowAuditQueryHandler.Handle [L87–L305]
      └─ uses_service GetAccountsQuery
        └─ method Execute [L247]
      └─ uses_service GetAccountTypesQuery
        └─ method Execute [L244]
      └─ uses_service GetCashflowCategoriesQuery
        └─ method Execute [L225]
      └─ uses_service GetCashflowJournalLinesQuery
        └─ method Execute [L236]
      └─ uses_service GetCashflowLinesQuery
        └─ method Execute [L226]
      └─ uses_service GetCashflowReconciliationLinesQuery
        └─ method Execute [L233]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L250]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L213]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L122]

