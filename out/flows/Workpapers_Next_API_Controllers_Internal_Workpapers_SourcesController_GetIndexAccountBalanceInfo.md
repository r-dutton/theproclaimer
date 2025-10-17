[web] PUT /api/internal/workpapers/sources/account-change-notification/{fileId:guid}  (Workpapers.Next.API.Controllers.Internal.Workpapers.SourcesController.GetIndexAccountBalanceInfo)  [L29–L42] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ calls SourceRepository.WriteQuery [L34]
  └─ write Source [L34]
  └─ uses_service SourceRepository
    └─ method WriteQuery [L34]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.WriteQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Source writes=1 reads=0
    └─ services 1
      └─ SourceRepository

