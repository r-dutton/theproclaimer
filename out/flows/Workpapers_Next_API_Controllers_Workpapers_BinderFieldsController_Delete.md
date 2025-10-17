[web] DELETE /api/binder-fields/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Delete)  [L105–L114] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ calls BinderFieldRepository (methods: Remove,WriteQuery) [L111]
  └─ delete BinderField [L111]
    └─ reads_from BinderFields
  └─ write BinderField [L109]
    └─ reads_from BinderFields
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ BinderField writes=2 reads=0

