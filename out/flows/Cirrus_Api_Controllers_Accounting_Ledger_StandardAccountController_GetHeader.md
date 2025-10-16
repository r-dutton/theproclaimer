[web] GET /api/accounting/ledger/standard-accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetHeader)  [L134–L141] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardHeaderDto [L137]
  └─ calls StandardAccountRepository.ReadQuery [L137]
  └─ query StandardAccount [L137]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method ReadQuery [L137]
      └─ ... (no implementation details available)

