[web] PUT /api/reportsettings/policies/options/{id}/confirm  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.ConfirmPolicy)  [L202–L213] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method GetFirmId [L207]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service UnitOfWork
    └─ method Table [L206]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

