[web] PUT /api/binder-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Update)  [L101–L111] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderTemplateRepository.WriteQuery [L107]
  └─ write BinderTemplate [L107]
    └─ reads_from BinderTemplates
  └─ uses_service IControlledRepository<BinderTemplate>
    └─ method WriteQuery [L107]
      └─ ... (no implementation details available)

