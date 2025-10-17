[web] GET /workpapers/v1/sources/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourcesController.GetSourcesMinimalQuery)  [L57–L63] status=200
  └─ calls SourceRepository.ReadQuery [L60]
  └─ query Source [L60]
  └─ uses_service SourceRepository
    └─ method ReadQuery [L60]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Source writes=0 reads=1
    └─ services 1
      └─ SourceRepository

