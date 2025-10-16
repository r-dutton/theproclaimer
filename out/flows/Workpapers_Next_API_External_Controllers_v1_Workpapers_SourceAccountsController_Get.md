[web] GET /workpapers/v1/source-accounts/{sourceAccountId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourceAccountsController.Get)  [L43–L49] status=200
  └─ maps_to SourceAccountDto [L46]
    └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountDto) [L625]
    └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountDto) [L256]
    └─ automapper.registration ExternalApiMappingProfile (SourceAccount->SourceAccountDto) [L105]
  └─ calls SourceAccountRepository.ReadQuery [L46]
  └─ query SourceAccount [L46]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L46]
      └─ ... (no implementation details available)

