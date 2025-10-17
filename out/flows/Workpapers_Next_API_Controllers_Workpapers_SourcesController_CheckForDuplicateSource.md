[web] GET /api/sources  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.CheckForDuplicateSource)  [L540–L560] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceRepository.ReadQuery [L548]
  └─ query Source [L548]
  └─ uses_service SourceRepository
    └─ method ReadQuery [L548]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Source writes=0 reads=1
    └─ services 1
      └─ SourceRepository

