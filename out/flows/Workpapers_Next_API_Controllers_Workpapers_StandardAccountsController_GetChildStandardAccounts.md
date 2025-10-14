[web] GET /api/standard-accounts  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.GetChildStandardAccounts)  [L218–L235]
  └─ calls SourceAccountRepository.ReadQuery [L228]
  └─ queries SourceAccount [L228]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L228]
  └─ sends_request GetChildStandardAccountsAsLightDtoQuery [L222]

