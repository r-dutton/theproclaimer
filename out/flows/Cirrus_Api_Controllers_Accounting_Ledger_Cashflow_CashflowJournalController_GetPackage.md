[web] GET /api/accounting/ledger/cashflow/journals/package  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.GetPackage)  [L71–L75] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetCashflowJournalPackageQuery [L74]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetCashflowJournalPackageQueryHandler.Handle [L46–L118]
      └─ uses_service GetCashflowLinesQuery
        └─ method Execute [L82]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowReconciliationLinesQuery
        └─ method Execute [L87]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowSimpleLinesQuery
        └─ method Execute [L92]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<CashflowJournal>
        └─ method ReadQuery [L99]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L69]
          └─ ... (no implementation details available)

