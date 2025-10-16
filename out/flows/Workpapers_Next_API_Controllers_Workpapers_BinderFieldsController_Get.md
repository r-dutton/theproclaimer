[web] GET /api/binder-fields/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Get)  [L61–L69] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to BinderFieldDto [L65]
    └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
  └─ calls BinderFieldRepository.ReadQuery [L65]
  └─ query BinderField [L65]
    └─ reads_from BinderFields
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderField writes=0 reads=1
    └─ mappings 1
      └─ BinderFieldDto

