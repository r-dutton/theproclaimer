[web] DELETE /api/accounting/ledger/cashflow/journals/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.Delete)  [L101–L107] status=200 [auth=Authentication.UserPolicy]
  └─ calls CashflowJournalRepository (methods: Remove,WriteQuery) [L105]
  └─ delete CashflowJournal [L105]
    └─ reads_from CashflowJournals
  └─ write CashflowJournal [L104]
    └─ reads_from CashflowJournals
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ CashflowJournal writes=2 reads=0

