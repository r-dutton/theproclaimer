[web] PUT /api/binder-fields/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Update)  [L88–L103] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ calls BinderFieldRepository (methods: ReadQuery,WriteQuery) [L94]
  └─ query BinderField [L94]
    └─ reads_from BinderFields
  └─ write BinderField [L92]
    └─ reads_from BinderFields
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ BinderField writes=1 reads=1

