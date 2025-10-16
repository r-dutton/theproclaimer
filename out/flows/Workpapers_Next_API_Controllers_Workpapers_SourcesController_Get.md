[web] GET /api/sources/{id}  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.Get)  [L78–L86] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to SourceDto [L81]
    └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
    └─ automapper.registration CirrusMappingProfile (Source->SourceDto) [L216]
    └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
  └─ calls SourceRepository.ReadQuery [L81]
  └─ query Source [L81]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L81]
      └─ ... (no implementation details available)

