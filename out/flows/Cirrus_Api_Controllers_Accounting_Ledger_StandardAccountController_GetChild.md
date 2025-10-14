[web] GET /api/accounting/ledger/standard-accounts/child/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetChild)  [L96–L103] [auth=Authentication.UserPolicy]
  └─ maps_to StandardChildDto [L99]
  └─ calls StandardAccountRepository.ReadQuery [L99]
  └─ queries StandardAccount [L99]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method ReadQuery [L99]

