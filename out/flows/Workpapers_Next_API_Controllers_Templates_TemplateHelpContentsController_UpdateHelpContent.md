[web] PUT /api/template-help-contents/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateHelpContentsController.UpdateHelpContent)  [L71–L82] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateHelpContentDto [L81]
  └─ calls TemplateHelpContentRepository.WriteQuery [L74]
  └─ write TemplateHelpContent [L74]
    └─ reads_from TemplateHelpContents
  └─ uses_service UserService
    └─ method IsSuperUser [L77]
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

