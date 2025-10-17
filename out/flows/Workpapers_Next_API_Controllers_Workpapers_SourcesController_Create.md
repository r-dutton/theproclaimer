[web] POST /api/sources/  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.Create)  [L118–L135] status=201 [auth=AuthorizationPolicies.User]
  └─ calls SourceRepository.Add [L132]
  └─ calls SourceTypesRepository.ReadQuery [L123]
  └─ insert Source [L132]
  └─ query SourceType [L123]
  └─ uses_service SourceRepository
    └─ method Add [L132]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.Add [L8-L40]
  └─ uses_service SourceTypesRepository
    └─ method ReadQuery [L123]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceTypesRepository.ReadQuery [L8-L38]
  └─ impact_summary
    └─ entities 2 (writes=1, reads=1)
      └─ Source writes=1 reads=0
      └─ SourceType writes=0 reads=1
    └─ services 2
      └─ SourceRepository
      └─ SourceTypesRepository

