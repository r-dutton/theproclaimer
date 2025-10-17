[web] DELETE /api/accounting/ledger/journals  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.DeleteJournals)  [L249–L261] status=200 [auth=user]
  └─ calls JournalRepository.WriteQuery [L253]
  └─ write Journal [L253]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Journal writes=1 reads=0

