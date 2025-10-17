[web] GET /workpapers/v1/matters/{matterId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.Get)  [L43–L49] status=200
  └─ maps_to MatterDto [L46]
    └─ automapper.registration WorkpapersMappingProfile (Matter->MatterDto) [L781]
    └─ automapper.registration ExternalApiMappingProfile (Matter->MatterDto) [L206]
  └─ calls MatterRepository.ReadQuery [L46]
  └─ query Matter [L46]
    └─ reads_from Matters
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Matter writes=0 reads=1
    └─ mappings 1
      └─ MatterDto

