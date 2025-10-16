[web] GET /api/binder-templates/  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Search)  [L75–L80] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetBinderTemplatesQuery [L79]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.BinderTemplates.GetBinderTemplatesQueryHandler.Handle [L47–L136]
      └─ uses_service UserService
        └─ method IsSuperUser [L96]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
      └─ uses_service IControlledRepository<BinderTemplate>
        └─ method ReadQuery [L98]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ExcludedBinderTemplate>
        └─ method ReadQuery [L132]
          └─ ... (no implementation details available)

