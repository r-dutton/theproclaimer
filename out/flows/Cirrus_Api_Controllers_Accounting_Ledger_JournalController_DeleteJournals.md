[web] DELETE /api/accounting/ledger/journals  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.DeleteJournals)  [L249–L261] status=200 [auth=user]
  └─ calls JournalRepository.WriteQuery [L253]
  └─ write Journal [L253]
  └─ uses_service IControlledRepository<Journal>
    └─ method WriteQuery [L253]
      └─ ... (no implementation details available)

