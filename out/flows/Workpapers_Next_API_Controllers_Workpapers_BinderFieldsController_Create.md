[web] POST /api/binder-fields/  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Create)  [L71–L86] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ calls BinderFieldRepository (methods: Add,ReadQuery) [L83]
  └─ insert BinderField [L83]
    └─ reads_from BinderFields
  └─ query BinderField [L75]
    └─ reads_from BinderFields
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ BinderField writes=1 reads=1

