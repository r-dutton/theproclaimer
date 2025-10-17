[web] GET /api/binder-fields/  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.GetAll)  [L50–L58] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to BinderFieldDto [L54]
    └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
  └─ calls BinderFieldRepository.ReadQuery [L54]
  └─ query BinderField [L54]
    └─ reads_from BinderFields
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderField writes=0 reads=1
    └─ mappings 1
      └─ BinderFieldDto

