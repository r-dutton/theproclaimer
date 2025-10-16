[web] GET /api/accounting/ledger/cashflow/journals/package  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.GetPackage)  [L71–L75] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetCashflowJournalPackageQuery -> GetCashflowJournalPackageQueryHandler [L74]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetCashflowJournalPackageQueryHandler.Handle [L46–L118]
      └─ uses_service IControlledRepository<CashflowJournal> (Scoped (inferred))
        └─ method ReadQuery [L99]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.Cashflow.CashflowJournalRepository.ReadQuery
      └─ uses_service GetCashflowSimpleLinesQuery
        └─ method Execute [L92]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowReconciliationLinesQuery
        └─ method Execute [L87]
          └─ ... (no implementation details available)
      └─ uses_service GetCashflowLinesQuery
        └─ method Execute [L82]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L69]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetCashflowJournalPackageQuery
    └─ handlers 1
      └─ GetCashflowJournalPackageQueryHandler

