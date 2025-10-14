[web] GET /api/binder-fields/  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.GetAll)  [L50–L58] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to BinderFieldDto [L54]
    └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
  └─ calls BinderFieldRepository.ReadQuery [L54]
  └─ queries BinderField [L54]
    └─ reads_from BinderFields
  └─ uses_service IControlledRepository<BinderField>
    └─ method ReadQuery [L54]

