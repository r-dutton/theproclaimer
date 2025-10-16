[web] DELETE /api/accounting/ledger/cashflow/journals/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.Delete)  [L101–L107] status=200 [auth=Authentication.UserPolicy]
  └─ calls CashflowJournalRepository.Remove [L105]
  └─ calls CashflowJournalRepository.WriteQuery [L104]
  └─ delete CashflowJournal [L105]
    └─ reads_from CashflowJournals
  └─ write CashflowJournal [L104]
    └─ reads_from CashflowJournals
  └─ uses_service IControlledRepository<CashflowJournal>
    └─ method WriteQuery [L104]
      └─ ... (no implementation details available)

