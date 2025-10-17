[web] PUT /api/firms/firm-account-types  (Workpapers.Next.API.Controllers.Firms.FirmsController.FirmAccountTypes)  [L209–L222] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls FirmRepository.WriteQuery [L215]
  └─ write Firm [L215]
    └─ reads_from Firms
  └─ uses_service UserService
    └─ method GetFirmId [L213]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Firm writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

