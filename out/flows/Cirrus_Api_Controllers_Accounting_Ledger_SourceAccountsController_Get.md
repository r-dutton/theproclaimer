[web] GET /api/accounting/ledger/source-accounts/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.Get)  [L52–L57] [auth=user]
  └─ maps_to SourceAccountDto [L55]
    └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountDto) [L625]
    └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountDto) [L256]
    └─ automapper.registration ExternalApiMappingProfile (SourceAccount->SourceAccountDto) [L105]
  └─ calls SourceAccountRepository.ReadQuery [L55]
  └─ queries SourceAccount [L55]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L55]

