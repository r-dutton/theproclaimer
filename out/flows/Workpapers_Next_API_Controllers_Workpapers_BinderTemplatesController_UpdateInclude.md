[web] PUT /api/binder-templates/{id:Guid}/include/{include:bool}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.UpdateInclude)  [L129–L140] [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderTemplateRepository.WriteQuery [L133]
  └─ writes_to BinderTemplate [L133]
    └─ reads_from BinderTemplates
  └─ uses_service IControlledRepository<BinderTemplate>
    └─ method WriteQuery [L133]

