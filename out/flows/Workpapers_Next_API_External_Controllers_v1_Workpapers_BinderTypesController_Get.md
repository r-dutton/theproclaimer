[web] GET /workpapers/v1/binder-types/{binderTypeId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BinderTypesController.Get)  [L42–L48] status=200
  └─ maps_to BinderTypeDto [L45]
    └─ automapper.registration ExternalApiMappingProfile (BinderType->BinderTypeDto) [L79]
    └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeDto) [L761]
  └─ calls BinderTypeRepository.ReadQuery [L45]
  └─ query BinderType [L45]
    └─ reads_from BinderTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderType writes=0 reads=1
    └─ mappings 1
      └─ BinderTypeDto

