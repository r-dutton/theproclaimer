[web] GET /api/binder-fields/for-binder-type/{binderTypeId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.GetAllByProduct)  [L36–L48] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to BinderFieldDto [L40]
    └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
  └─ calls BinderFieldRepository.ReadQuery [L40]
  └─ query BinderField [L40]
    └─ reads_from BinderFields
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderField writes=0 reads=1
    └─ mappings 1
      └─ BinderFieldDto

