[web] GET /api/binder-types/  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetAll)  [L48–L54] status=200
  └─ sends_request GetValidBinderTypesQuery [L51]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetValidBinderTypesQueryHandler.Handle [L31–L77]
      └─ maps_to BinderTypeLightDto [L69]
        └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeLightDto) [L762]
      └─ uses_service IControlledRepository<BinderTemplate>
        └─ method ReadQuery [L57]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<BinderType>
        └─ method ReadQuery [L69]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ExcludedBinderTemplate>
        └─ method ReadQuery [L55]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L72]
          └─ ... (no implementation details available)

