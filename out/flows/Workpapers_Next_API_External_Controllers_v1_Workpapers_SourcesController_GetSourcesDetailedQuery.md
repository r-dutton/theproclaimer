[web] GET /workpapers/v1/sources/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourcesController.GetSourcesDetailedQuery)  [L75–L81] status=200
  └─ calls SourceRepository.ReadQuery [L78]
  └─ query Source [L78]
  └─ uses_service SourceRepository
    └─ method ReadQuery [L78]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Source writes=0 reads=1
    └─ services 1
      └─ SourceRepository

