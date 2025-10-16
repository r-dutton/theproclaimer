[web] PUT /api/matter-text-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Update)  [L153–L165] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTextTemplateRepository.WriteQuery [L157]
  └─ write MatterTextTemplate [L157]
    └─ reads_from MatterTextTemplates
  └─ uses_service UserService
    └─ method IsSuperUser [L159]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ MatterTextTemplate writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

