[web] GET /api/binder-roll-over/range-values  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.GetRangeValues)  [L51–L62] [auth=AuthorizationPolicies.User]
  └─ uses_service StorageService
    └─ method GetStream [L56]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L54]

