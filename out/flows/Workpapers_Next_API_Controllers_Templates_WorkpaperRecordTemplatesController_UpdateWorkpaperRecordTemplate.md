[web] PUT /api/workpaper-record-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.UpdateWorkpaperRecordTemplate)  [L116–L137] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls WorkpaperRecordTemplateRepository.WriteQuery [L121]
  └─ write WorkpaperRecordTemplate [L121]
    └─ reads_from WorkpaperTemplates
  └─ uses_service UserService
    └─ method IsSuperUser [L123]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request UpdateWorkpaperRecordTemplateCommand [L134]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ WorkpaperRecordTemplate writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ UpdateWorkpaperRecordTemplateCommand
    └─ caches 1
      └─ IMemoryCache

