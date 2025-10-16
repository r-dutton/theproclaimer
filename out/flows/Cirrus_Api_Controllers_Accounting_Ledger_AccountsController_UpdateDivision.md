[web] PUT /api/accounting/ledger/accounts/division/{id:guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UpdateDivision)  [L405–L411] status=200 [auth=user]
  └─ calls AccountRepository.WriteQuery [L408]
  └─ write Account [L408]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L408]
      └─ ... (no implementation details available)

