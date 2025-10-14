[web] GET /api/binder-types/  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetAll)  [L48–L54]
  └─ sends_request GetValidBinderTypesQuery [L51]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetValidBinderTypesQueryHandler.Handle [L31–L77]
      └─ maps_to BinderTypeLightDto [L69]
        └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeLightDto) [L762]
      └─ uses_service IControlledRepository<BinderTemplate>
        └─ method ReadQuery [L57]
      └─ uses_service IControlledRepository<BinderType>
        └─ method ReadQuery [L69]
      └─ uses_service IControlledRepository<ExcludedBinderTemplate>
        └─ method ReadQuery [L55]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L72]

