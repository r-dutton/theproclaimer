[web] PUT /api/binder-templates/{id:Guid}/include/{include:bool}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.UpdateInclude)  [L129–L140] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderTemplateRepository.WriteQuery [L133]
  └─ write BinderTemplate [L133]
    └─ reads_from BinderTemplates
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ BinderTemplate writes=1 reads=0

