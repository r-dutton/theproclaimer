[web] GET /api/binder-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Get)  [L49–L73] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to BinderRecordTemplateDto [L59]
  └─ maps_to BinderTemplateDto [L55]
    └─ automapper.registration WorkpapersMappingProfile (BinderTemplate->BinderTemplateDto) [L726]
  └─ calls ExcludedBinderTemplateRepository.ReadQuery [L70]
  └─ calls BinderTemplateRepository.ReadQuery [L55]
  └─ query ExcludedBinderTemplate [L70]
    └─ reads_from ExcludedBinderTemplates
  └─ query BinderTemplate [L55]
    └─ reads_from BinderTemplates
  └─ uses_service UserService
    └─ method IsSuperUser [L53]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ BinderTemplate writes=0 reads=1
      └─ ExcludedBinderTemplate writes=0 reads=1
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ BinderRecordTemplateDto
      └─ BinderTemplateDto

