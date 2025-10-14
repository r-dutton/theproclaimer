[web] PUT /api/source-accounts/bulk  (Workpapers.Next.API.Controllers.SourceAccountsController.UpdateBulk)  [L205–L224] [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.WriteQuery [L208]
  └─ writes_to SourceAccount [L208]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method WriteQuery [L208]

