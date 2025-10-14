[web] GET /workpapers/v1/matters/{matterId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.Get)  [L43–L49]
  └─ maps_to MatterDto [L46]
    └─ automapper.registration ExternalApiMappingProfile (Matter->MatterDto) [L206]
    └─ automapper.registration WorkpapersMappingProfile (Matter->MatterDto) [L781]
  └─ calls MatterRepository.ReadQuery [L46]
  └─ queries Matter [L46]
    └─ reads_from Matters
  └─ uses_service IControlledRepository<Matter>
    └─ method ReadQuery [L46]

