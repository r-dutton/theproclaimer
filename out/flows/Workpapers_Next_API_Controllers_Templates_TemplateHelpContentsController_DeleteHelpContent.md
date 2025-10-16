[web] DELETE /api/template-help-contents/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.DeleteHelpContent)  [L84–L95] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls TemplateHelpContentRepository (methods: Remove,WriteQuery) [L92]
  └─ delete TemplateHelpContent [L92]
    └─ reads_from TemplateHelpContents
  └─ write TemplateHelpContent [L87]
    └─ reads_from TemplateHelpContents
  └─ uses_service UserService
    └─ method IsSuperUser [L90]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ TemplateHelpContent writes=2 reads=0
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

