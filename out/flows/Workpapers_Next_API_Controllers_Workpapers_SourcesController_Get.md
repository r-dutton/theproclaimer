[web] GET /api/sources/{id}  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.Get)  [L78–L86] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to SourceDto [L81]
    └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
    └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
  └─ calls SourceRepository.ReadQuery [L81]
  └─ query Source [L81]
  └─ uses_service SourceRepository
    └─ method ReadQuery [L81]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Source writes=0 reads=1
    └─ services 1
      └─ SourceRepository
    └─ mappings 1
      └─ SourceDto

