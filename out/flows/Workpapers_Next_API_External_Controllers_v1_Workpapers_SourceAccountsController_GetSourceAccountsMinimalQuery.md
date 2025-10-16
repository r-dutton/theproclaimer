[web] GET /workpapers/v1/source-accounts/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourceAccountsController.GetSourceAccountsMinimalQuery)  [L57–L63] status=200
  └─ calls SourceAccountRepository.ReadQuery [L60]
  └─ query SourceAccount [L60]
    └─ reads_from SourceAccounts
  └─ uses_service SourceAccountRepository
    └─ method ReadQuery [L60]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.ReadQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SourceAccount writes=0 reads=1
    └─ services 1
      └─ SourceAccountRepository

