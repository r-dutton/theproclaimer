[web] PUT /api/source-accounts/{id:guid}/reset-source-system-id  (Workpapers.Next.API.Controllers.SourceAccountsController.ResetSourceSystemId)  [L190–L197] [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.WriteQuery [L193]
  └─ writes_to SourceAccount [L193]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method WriteQuery [L193]

