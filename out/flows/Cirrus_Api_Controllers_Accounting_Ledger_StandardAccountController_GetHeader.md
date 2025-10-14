[web] GET /api/accounting/ledger/standard-accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetHeader)  [L134–L141] [auth=Authentication.UserPolicy]
  └─ maps_to StandardHeaderDto [L137]
  └─ calls StandardAccountRepository.ReadQuery [L137]
  └─ queries StandardAccount [L137]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method ReadQuery [L137]

