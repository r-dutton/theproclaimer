[web] GET /api/accounting/ledger/cashflow/journals/package  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.GetPackage)  [L71–L75] [auth=Authentication.UserPolicy]
  └─ sends_request GetCashflowJournalPackageQuery [L74]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetCashflowJournalPackageQueryHandler.Handle [L46–L118]
      └─ uses_service GetCashflowLinesQuery
        └─ method Execute [L82]
      └─ uses_service GetCashflowReconciliationLinesQuery
        └─ method Execute [L87]
      └─ uses_service GetCashflowSimpleLinesQuery
        └─ method Execute [L92]
      └─ uses_service IControlledRepository<CashflowJournal>
        └─ method ReadQuery [L99]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L69]

