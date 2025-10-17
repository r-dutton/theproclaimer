[web] PUT /api/workpaper-record-templates/{id:Guid}/include/{include:bool}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.UpdateIncludes)  [L139–L158] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls WorkpaperRecordTemplateRepository.WriteQuery [L146]
  └─ write WorkpaperRecordTemplate [L146]
    └─ reads_from WorkpaperTemplates
  └─ uses_service UserService
    └─ method GetFirmId [L144]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ WorkpaperRecordTemplate writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

