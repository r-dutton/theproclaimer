[web] GET /api/binder-roll-over/stored-binder  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.GetBinder)  [L64–L74] [auth=AuthorizationPolicies.User]
  └─ uses_service StorageService
    └─ method GetStream [L70]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L67]

