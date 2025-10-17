[web] GET /api/accounting/ledger/source-accounts/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.Get)  [L52–L57] status=200 [auth=user]
  └─ maps_to SourceAccountDto [L55]
    └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountDto) [L256]
  └─ calls SourceAccountRepository.ReadQuery [L55]
  └─ query SourceAccount [L55]
    └─ reads_from SourceAccounts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SourceAccount writes=0 reads=1
    └─ mappings 1
      └─ SourceAccountDto

