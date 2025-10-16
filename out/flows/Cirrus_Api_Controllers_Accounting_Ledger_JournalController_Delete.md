[web] DELETE /api/accounting/ledger/journals/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.Delete)  [L238–L244] status=200 [auth=user]
  └─ calls JournalRepository.WriteQuery [L241]
  └─ write Journal [L241]
  └─ uses_service IControlledRepository<Journal>
    └─ method WriteQuery [L241]
      └─ ... (no implementation details available)

