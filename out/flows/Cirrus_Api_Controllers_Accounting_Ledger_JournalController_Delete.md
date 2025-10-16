[web] DELETE /api/accounting/ledger/journals/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.Delete)  [L238–L244] status=200 [auth=user]
  └─ calls JournalRepository.WriteQuery [L241]
  └─ write Journal [L241]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Journal writes=1 reads=0

