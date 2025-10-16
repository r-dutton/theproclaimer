[web] GET /workpapers/v1/binder-types/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BinderTypesController.GetBinderTypesQuery)  [L55–L61] status=200
  └─ maps_to BinderTypeDto [L58]
    └─ automapper.registration ExternalApiMappingProfile (BinderType->BinderTypeDto) [L79]
    └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeDto) [L761]
  └─ calls BinderTypeRepository.ReadQuery [L58]
  └─ query BinderType [L58]
    └─ reads_from BinderTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderType writes=0 reads=1
    └─ mappings 1
      └─ BinderTypeDto

