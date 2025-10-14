[web] GET /api/binder-fields/for-binder-type/{binderTypeId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.GetAllByProduct)  [L36–L48] [auth=AuthorizationPolicies.User]
  └─ maps_to BinderFieldDto [L40]
    └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
  └─ calls BinderFieldRepository.ReadQuery [L40]
  └─ queries BinderField [L40]
    └─ reads_from BinderFields
  └─ uses_service IControlledRepository<BinderField>
    └─ method ReadQuery [L40]

