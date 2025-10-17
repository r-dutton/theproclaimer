[web] GET /workpapers/v1/matters/{matterId:Guid}/messages  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMatterWithMessages)  [L57–L63] status=200
  └─ maps_to MatterWithMessagesDto [L60]
    └─ automapper.registration WorkpapersMappingProfile (Matter->MatterWithMessagesDto) [L784]
    └─ automapper.registration ExternalApiMappingProfile (Matter->MatterWithMessagesDto) [L210]
  └─ calls MatterRepository.ReadQuery [L60]
  └─ query Matter [L60]
    └─ reads_from Matters
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Matter writes=0 reads=1
    └─ mappings 1
      └─ MatterWithMessagesDto

