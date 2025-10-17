[web] PUT /api/binder-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Update)  [L101–L111] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderTemplateRepository.WriteQuery [L107]
  └─ write BinderTemplate [L107]
    └─ reads_from BinderTemplates
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ BinderTemplate writes=1 reads=0

