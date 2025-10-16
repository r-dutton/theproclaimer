[web] PUT /api/binder-fields/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Update)  [L88–L103] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ calls BinderFieldRepository.ReadQuery [L94]
  └─ calls BinderFieldRepository.WriteQuery [L92]
  └─ query BinderField [L94]
    └─ reads_from BinderFields
  └─ write BinderField [L92]
    └─ reads_from BinderFields
  └─ uses_service IControlledRepository<BinderField>
    └─ method WriteQuery [L92]
      └─ ... (no implementation details available)

