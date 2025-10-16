[web] GET /api/binder-fields/  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.GetAll)  [L50–L58] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to BinderFieldDto [L54]
    └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
  └─ calls BinderFieldRepository.ReadQuery [L54]
  └─ query BinderField [L54]
    └─ reads_from BinderFields
  └─ uses_service IControlledRepository<BinderField>
    └─ method ReadQuery [L54]
      └─ ... (no implementation details available)

