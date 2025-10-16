[web] PUT /api/source-accounts/{id:guid}/reset-source-system-id  (Workpapers.Next.API.Controllers.SourceAccountsController.ResetSourceSystemId)  [L190–L197] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.WriteQuery [L193]
  └─ write SourceAccount [L193]
    └─ reads_from SourceAccounts
  └─ uses_service SourceAccountRepository
    └─ method WriteQuery [L193]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.WriteQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SourceAccount writes=1 reads=0
    └─ services 1
      └─ SourceAccountRepository

