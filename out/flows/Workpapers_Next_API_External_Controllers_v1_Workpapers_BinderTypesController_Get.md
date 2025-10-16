[web] GET /workpapers/v1/binder-types/{binderTypeId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BinderTypesController.Get)  [L42–L48] status=200
  └─ maps_to BinderTypeDto [L45]
    └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeDto) [L761]
    └─ automapper.registration ExternalApiMappingProfile (BinderType->BinderTypeDto) [L79]
  └─ calls BinderTypeRepository.ReadQuery [L45]
  └─ query BinderType [L45]
    └─ reads_from BinderTypes
  └─ uses_service IControlledRepository<BinderType>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)

