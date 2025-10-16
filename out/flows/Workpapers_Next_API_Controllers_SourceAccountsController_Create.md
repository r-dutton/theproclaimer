[web] POST /api/source-accounts/  (Workpapers.Next.API.Controllers.SourceAccountsController.Create)  [L122–L149] status=201 [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository (methods: Add,WriteQuery) [L146]
  └─ calls SourceRepository.WriteQuery [L127]
  └─ insert SourceAccount [L146]
    └─ reads_from SourceAccounts
  └─ write SourceAccount [L130]
    └─ reads_from SourceAccounts
  └─ write Source [L127]
  └─ uses_service SourceAccountRepository
    └─ method WriteQuery [L130]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.WriteQuery [L8-L38]
  └─ uses_service SourceRepository
    └─ method WriteQuery [L127]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.WriteQuery [L8-L40]
  └─ impact_summary
    └─ entities 2 (writes=3, reads=0)
      └─ SourceAccount writes=2 reads=0
      └─ Source writes=1 reads=0
    └─ services 2
      └─ SourceAccountRepository
      └─ SourceRepository

