[web] DELETE /api/matter-text-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Delete)  [L167–L179] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTextTemplateRepository (methods: Remove,WriteQuery) [L176]
  └─ delete MatterTextTemplate [L176]
    └─ reads_from MatterTextTemplates
  └─ write MatterTextTemplate [L171]
    └─ reads_from MatterTextTemplates
  └─ uses_service UserService
    └─ method IsSuperUser [L173]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ MatterTextTemplate writes=2 reads=0
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

