[web] GET /api/standard-accounts  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.GetChildStandardAccounts)  [L218–L235] status=200
  └─ calls SourceAccountRepository.ReadQuery [L228]
  └─ query SourceAccount [L228]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L228]
      └─ ... (no implementation details available)
  └─ sends_request GetChildStandardAccountsAsLightDtoQuery [L222]

