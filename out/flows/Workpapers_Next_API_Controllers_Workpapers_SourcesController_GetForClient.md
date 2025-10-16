[web] GET /api/sources/for-client/{clientId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetForClient)  [L92–L102] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to SourceDto [L95]
    └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
    └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
  └─ calls SourceRepository.ReadQuery [L95]
  └─ query Source [L95]
  └─ uses_service SourceRepository
    └─ method ReadQuery [L95]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Source writes=0 reads=1
    └─ services 1
      └─ SourceRepository
    └─ mappings 1
      └─ SourceDto

