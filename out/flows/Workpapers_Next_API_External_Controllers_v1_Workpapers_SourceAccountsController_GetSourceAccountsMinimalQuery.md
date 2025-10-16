[web] GET /workpapers/v1/source-accounts/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourceAccountsController.GetSourceAccountsMinimalQuery)  [L57–L63] status=200
  └─ calls SourceAccountRepository.ReadQuery [L60]
  └─ query SourceAccount [L60]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L60]
      └─ ... (no implementation details available)

