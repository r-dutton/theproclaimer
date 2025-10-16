[web] POST /api/source-accounts/  (Workpapers.Next.API.Controllers.SourceAccountsController.Create)  [L122–L149] status=201 [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.Add [L146]
  └─ calls SourceAccountRepository.WriteQuery [L130]
  └─ calls SourceRepository.WriteQuery [L127]
  └─ insert SourceAccount [L146]
    └─ reads_from SourceAccounts
  └─ write SourceAccount [L130]
    └─ reads_from SourceAccounts
  └─ write Source [L127]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L127]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method WriteQuery [L130]
      └─ ... (no implementation details available)

