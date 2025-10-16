[web] POST /api/binder-templates/  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Create)  [L89–L99] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderTemplateRepository.Add [L96]
  └─ insert BinderTemplate [L96]
    └─ reads_from BinderTemplates
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ BinderTemplate writes=1 reads=0

