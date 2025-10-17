[web] GET /workpapers/v1/sources/{sourceId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourcesController.Get)  [L43–L49] status=200
  └─ maps_to SourceDto [L46]
    └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
    └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
  └─ calls SourceRepository.ReadQuery [L46]
  └─ query Source [L46]
  └─ uses_service SourceRepository
    └─ method ReadQuery [L46]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Source writes=0 reads=1
    └─ services 1
      └─ SourceRepository
    └─ mappings 1
      └─ SourceDto

