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
              └─ uses_service RequestProcessor
                └─ method GetCatalogByFirmId [L104]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                  └─ +1 additional_requests elided
              └─ uses_service FirmLightDto
                └─ method AssignActiveTenant [L77]
                  └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
  └─ uses_storage StorageService.GetStream [L70]
  └─ impact_summary
    └─ services 2
      └─ StorageService
      └─ TenantService
    └─ caches 1
      └─ IMemoryCache
    └─ storages 1
      └─ StorageService

