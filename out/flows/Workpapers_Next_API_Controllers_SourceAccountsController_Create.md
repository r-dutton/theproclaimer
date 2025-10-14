[web] POST /api/source-accounts/  (Workpapers.Next.API.Controllers.SourceAccountsController.Create)  [L122–L149] [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.Add [L146]
  └─ calls SourceAccountRepository.WriteQuery [L130]
  └─ calls SourceRepository.WriteQuery [L127]
  └─ writes_to SourceAccount [L146]
    └─ reads_from SourceAccounts
  └─ writes_to SourceAccount [L130]
    └─ reads_from SourceAccounts
  └─ writes_to Source [L127]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L127]
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method WriteQuery [L130]

