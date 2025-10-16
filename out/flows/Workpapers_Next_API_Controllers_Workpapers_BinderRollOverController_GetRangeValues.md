[web] GET /api/binder-roll-over/range-values  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.GetRangeValues)  [L51–L62] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service StorageService
    └─ method GetStream [L56]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.GetStream [L17-L282]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L54]
      └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
  └─ uses_storage StorageService.GetStream [L56]

