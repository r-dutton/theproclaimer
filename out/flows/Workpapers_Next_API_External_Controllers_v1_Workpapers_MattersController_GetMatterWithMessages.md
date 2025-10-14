[web] GET /workpapers/v1/matters/{matterId:Guid}/messages  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMatterWithMessages)  [L57–L63]
  └─ maps_to MatterWithMessagesDto [L60]
    └─ automapper.registration ExternalApiMappingProfile (Matter->MatterWithMessagesDto) [L210]
    └─ automapper.registration WorkpapersMappingProfile (Matter->MatterWithMessagesDto) [L784]
  └─ calls MatterRepository.ReadQuery [L60]
  └─ queries Matter [L60]
    └─ reads_from Matters
  └─ uses_service IControlledRepository<Matter>
    └─ method ReadQuery [L60]

