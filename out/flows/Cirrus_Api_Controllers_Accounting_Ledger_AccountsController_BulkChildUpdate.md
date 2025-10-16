[web] PUT /api/accounting/ledger/accounts/bulk-child-update  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.BulkChildUpdate)  [L302–L350] status=200 [auth=user]
  └─ calls AccountRepository.ReadQuery [L320]
  └─ calls AccountRepository.WriteQuery [L305]
  └─ query Account [L320]
  └─ write Account [L305]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L305]
      └─ ... (no implementation details available)

