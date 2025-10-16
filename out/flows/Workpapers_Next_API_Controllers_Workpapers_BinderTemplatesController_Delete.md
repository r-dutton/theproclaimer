[web] DELETE /api/binder-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Delete)  [L113–L127] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderTemplateRepository.WriteQuery [L119]
  └─ write BinderTemplate [L119]
    └─ reads_from BinderTemplates
  └─ uses_service IControlledRepository<BinderTemplate>
    └─ method WriteQuery [L119]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L117]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

