[web] PUT /api/source-accounts/bulk  (Workpapers.Next.API.Controllers.SourceAccountsController.UpdateBulk)  [L205–L224] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.WriteQuery [L208]
  └─ write SourceAccount [L208]
    └─ reads_from SourceAccounts
  └─ uses_service SourceAccountRepository
    └─ method WriteQuery [L208]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.WriteQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SourceAccount writes=1 reads=0
    └─ services 1
      └─ SourceAccountRepository

