[web] GET /workpapers/v1/source-accounts/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourceAccountsController.GetSourceAccountsDetailedQuery)  [L75–L81]
  └─ calls SourceAccountRepository.ReadQuery [L78]
  └─ queries SourceAccount [L78]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L78]

