[web] GET /workpapers/v1/binders/{binderId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.Get)  [L49–L54] status=200
  └─ maps_to BinderDto [L52]
    └─ automapper.registration ExternalApiMappingProfile (Binder->BinderDto) [L58]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderDto) [L450]
  └─ calls BinderRepository.ReadQuery [L52]
  └─ query Binder [L52]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L52]
      └─ ... (no implementation details available)

