[web] GET /api/binder-templates/  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Search)  [L75–L80] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetBinderTemplatesQuery -> GetBinderTemplatesQueryHandler [L79]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.BinderTemplates.GetBinderTemplatesQueryHandler.Handle [L47–L136]
      └─ uses_service IControlledRepository<ExcludedBinderTemplate> (Scoped (inferred))
        └─ method ReadQuery [L132]
          └─ implementation Workpapers.Next.Data.Repository.Templates.ExcludedBinderTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<BinderTemplate> (Scoped (inferred))
        └─ method ReadQuery [L98]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTemplateRepository.ReadQuery
      └─ uses_service UserService
        └─ method IsSuperUser [L96]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
            └─ uses_service bool?
              └─ method IsSuperUser [L174]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ requests 1
      └─ GetBinderTemplatesQuery
    └─ handlers 1
      └─ GetBinderTemplatesQueryHandler
    └─ caches 1
      └─ IMemoryCache

