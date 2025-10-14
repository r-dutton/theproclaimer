[web] DELETE /api/accounting/ledger/journals  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.DeleteJournals)  [L249–L261] [auth=user]
  └─ calls JournalRepository.WriteQuery [L253]
  └─ writes_to Journal [L253]
  └─ uses_service IControlledRepository<Journal>
    └─ method WriteQuery [L253]

