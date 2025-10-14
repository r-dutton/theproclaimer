[web] DELETE /api/accounting/ledger/standard-accounts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.Delete)  [L194–L199] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.Remove [L198]
  └─ calls StandardAccountRepository.WriteQuery [L197]
  └─ writes_to StandardAccount [L198]
    └─ reads_from StandardAccounts
  └─ writes_to StandardAccount [L197]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method WriteQuery [L197]

