[web] DELETE /api/accounting/ledger/cashflow/journals/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.Delete)  [L101–L107] [auth=Authentication.UserPolicy]
  └─ calls CashflowJournalRepository.Remove [L105]
  └─ calls CashflowJournalRepository.WriteQuery [L104]
  └─ writes_to CashflowJournal [L105]
    └─ reads_from CashflowJournals
  └─ writes_to CashflowJournal [L104]
    └─ reads_from CashflowJournals
  └─ uses_service IControlledRepository<CashflowJournal>
    └─ method WriteQuery [L104]

