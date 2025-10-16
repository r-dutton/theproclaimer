[web] GET /api/accounting/ledger/standard-accounts/child/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.GetChild)  [L96–L103] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to StandardChildDto [L99]
  └─ calls StandardAccountRepository.ReadQuery [L99]
  └─ query StandardAccount [L99]
    └─ reads_from StandardAccounts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardAccount writes=0 reads=1
    └─ mappings 1
      └─ StandardChildDto

