[web] POST /api/binder-fields/  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Create)  [L71–L86] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ calls BinderFieldRepository.Add [L83]
  └─ calls BinderFieldRepository.ReadQuery [L75]
  └─ insert BinderField [L83]
    └─ reads_from BinderFields
  └─ query BinderField [L75]
    └─ reads_from BinderFields
  └─ uses_service IControlledRepository<BinderField>
    └─ method ReadQuery [L75]
      └─ ... (no implementation details available)

