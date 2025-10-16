[web] GET /api/accounting/ledger/standard-accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetHeader)  [L134–L141] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardHeaderDto [L137]
  └─ calls StandardAccountRepository.ReadQuery [L137]
  └─ query StandardAccount [L137]
    └─ reads_from StandardAccounts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardAccount writes=0 reads=1
    └─ mappings 1
      └─ StandardHeaderDto

