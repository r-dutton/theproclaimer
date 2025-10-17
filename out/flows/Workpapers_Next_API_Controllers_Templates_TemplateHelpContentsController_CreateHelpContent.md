[web] POST /api/template-help-contents  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.CreateHelpContent)  [L60–L69] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateHelpContentDto [L68]
  └─ calls TemplateHelpContentRepository.Add [L66]
  └─ insert TemplateHelpContent [L66]
    └─ reads_from TemplateHelpContents
  └─ uses_service UserService
    └─ method IsSuperUser [L65]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ TemplateHelpContent writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ TemplateHelpContentDto

