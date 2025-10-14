[web] GET /workpapers/v1/sources/{sourceId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.SourcesController.Get)  [L43–L49]
  └─ maps_to SourceDto [L46]
    └─ automapper.registration ExternalApiMappingProfile (Source->SourceDto) [L95]
    └─ automapper.registration CirrusMappingProfile (Source->SourceDto) [L216]
    └─ automapper.registration WorkpapersMappingProfile (Source->SourceDto) [L598]
  └─ calls SourceRepository.ReadQuery [L46]
  └─ queries Source [L46]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L46]

