[web] DELETE /api/accounting/ledger/journals/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.Delete)  [L238–L244] [auth=user]
  └─ calls JournalRepository.WriteQuery [L241]
  └─ writes_to Journal [L241]
  └─ uses_service IControlledRepository<Journal>
    └─ method WriteQuery [L241]

