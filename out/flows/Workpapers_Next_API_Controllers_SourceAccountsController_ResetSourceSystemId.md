[web] PUT /api/source-accounts/{id:guid}/reset-source-system-id  (Workpapers.Next.API.Controllers.SourceAccountsController.ResetSourceSystemId)  [L190–L197] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.WriteQuery [L193]
  └─ write SourceAccount [L193]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method WriteQuery [L193]
      └─ ... (no implementation details available)

