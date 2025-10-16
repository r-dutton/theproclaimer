[web] PUT /api/accounting/ledger/accounts/tax-properties/{id:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UpdateTaxData)  [L431–L442] status=200 [auth=user]
  └─ calls AccountRepository.WriteQuery [L439]
  └─ write Account [L439]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L439]
      └─ ... (no implementation details available)

