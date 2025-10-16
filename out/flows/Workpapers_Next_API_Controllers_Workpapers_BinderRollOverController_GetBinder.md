[web] GET /api/binder-roll-over/stored-binder  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.GetBinder)  [L64–L74] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service StorageService
    └─ method GetStream [L70]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.GetStream [L17-L282]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L67]
      └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
  └─ uses_storage StorageService.GetStream [L70]

