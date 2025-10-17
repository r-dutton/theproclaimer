[web] POST /api/matter-text-templates/  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Create)  [L140–L151] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTextTemplateRepository.Add [L148]
  └─ insert MatterTextTemplate [L148]
    └─ reads_from MatterTextTemplates
  └─ uses_service UserService
    └─ method IsSuperUser [L144]
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

