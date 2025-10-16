[web] DELETE /api/accounting/ledger/standard-accounts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.Delete)  [L194–L199] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.Remove [L198]
  └─ calls StandardAccountRepository.WriteQuery [L197]
  └─ delete StandardAccount [L198]
    └─ reads_from StandardAccounts
  └─ write StandardAccount [L197]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method WriteQuery [L197]
      └─ ... (no implementation details available)

