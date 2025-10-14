[web] GET /workpapers/v1/source-accounts/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourceAccountsController.GetSourceAccountsMinimalQuery)  [L57–L63]
  └─ calls SourceAccountRepository.ReadQuery [L60]
  └─ queries SourceAccount [L60]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L60]

