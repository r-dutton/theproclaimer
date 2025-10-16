[web] DELETE /api/binder-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderTemplatesController.Delete)  [L113–L127] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderTemplateRepository.WriteQuery [L119]
  └─ write BinderTemplate [L119]
    └─ reads_from BinderTemplates
  └─ uses_service UserService
    └─ method IsSuperUser [L117]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ BinderTemplate writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

