[web] GET /workpapers/v1/source-accounts/{sourceAccountId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourceAccountsController.Get)  [L43–L49] status=200
  └─ maps_to SourceAccountDto [L46]
    └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountDto) [L625]
    └─ automapper.registration ExternalApiMappingProfile (SourceAccount->SourceAccountDto) [L105]
  └─ calls SourceAccountRepository.ReadQuery [L46]
  └─ query SourceAccount [L46]
    └─ reads_from SourceAccounts
  └─ uses_service SourceAccountRepository
    └─ method ReadQuery [L46]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.ReadQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SourceAccount writes=0 reads=1
    └─ services 1
      └─ SourceAccountRepository
    └─ mappings 1
      └─ SourceAccountDto

