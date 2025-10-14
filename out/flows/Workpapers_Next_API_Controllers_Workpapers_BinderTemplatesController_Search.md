[web] GET /api/binder-templates/  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Search)  [L75–L80] [auth=AuthorizationPolicies.User]
  └─ sends_request GetBinderTemplatesQuery [L79]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.BinderTemplates.GetBinderTemplatesQueryHandler.Handle [L47–L136]
      └─ uses_service IControlledRepository<BinderTemplate>
        └─ method ReadQuery [L98]
      └─ uses_service IControlledRepository<ExcludedBinderTemplate>
        └─ method ReadQuery [L132]
      └─ uses_service UserService
        └─ method IsSuperUser [L96]

