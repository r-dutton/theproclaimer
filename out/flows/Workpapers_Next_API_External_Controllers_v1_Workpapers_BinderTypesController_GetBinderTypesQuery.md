[web] GET /workpapers/v1/binder-types/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BinderTypesController.GetBinderTypesQuery)  [L55–L61] status=200
  └─ maps_to BinderTypeDto [L58]
    └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeDto) [L761]
    └─ automapper.registration ExternalApiMappingProfile (BinderType->BinderTypeDto) [L79]
  └─ calls BinderTypeRepository.ReadQuery [L58]
  └─ query BinderType [L58]
    └─ reads_from BinderTypes
  └─ uses_service IControlledRepository<BinderType>
    └─ method ReadQuery [L58]
      └─ ... (no implementation details available)

