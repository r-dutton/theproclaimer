[web] PUT /api/source-accounts/bulk  (Workpapers.Next.API.Controllers.SourceAccountsController.UpdateBulk)  [L205–L224] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.WriteQuery [L208]
  └─ write SourceAccount [L208]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method WriteQuery [L208]
      └─ ... (no implementation details available)

