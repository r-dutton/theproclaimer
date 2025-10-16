[web] GET /api/binder-fields/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Get)  [L61–L69] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to BinderFieldDto [L65]
    └─ automapper.registration WorkpapersMappingProfile (BinderField->BinderFieldDto) [L428]
  └─ calls BinderFieldRepository.ReadQuery [L65]
  └─ query BinderField [L65]
    └─ reads_from BinderFields
  └─ uses_service IControlledRepository<BinderField>
    └─ method ReadQuery [L65]
      └─ ... (no implementation details available)

