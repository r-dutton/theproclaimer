[web] DELETE /api/binder-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Delete)  [L113–L127] [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderTemplateRepository.WriteQuery [L119]
  └─ writes_to BinderTemplate [L119]
    └─ reads_from BinderTemplates
  └─ uses_service IControlledRepository<BinderTemplate>
    └─ method WriteQuery [L119]
  └─ uses_service UserService
    └─ method IsSuperUser [L117]

