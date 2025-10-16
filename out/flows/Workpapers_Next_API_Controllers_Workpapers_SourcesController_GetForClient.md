[web] GET /api/sources/for-client/{clientId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetForClient)  [L92–L102] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to SourceDto [L95]
    └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
    └─ automapper.registration CirrusMappingProfile (Source->SourceDto) [L216]
    └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
  └─ calls SourceRepository.ReadQuery [L95]
  └─ query Source [L95]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L95]
      └─ ... (no implementation details available)

