[web] POST /api/binder-fields/  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Create)  [L71–L86] [auth=AuthorizationPolicies.SuperUser]
  └─ calls BinderFieldRepository.Add [L83]
  └─ calls BinderFieldRepository.ReadQuery [L75]
  └─ queries BinderField [L75]
    └─ reads_from BinderFields
  └─ writes_to BinderField [L83]
    └─ reads_from BinderFields
  └─ uses_service IControlledRepository<BinderField>
    └─ method ReadQuery [L75]

