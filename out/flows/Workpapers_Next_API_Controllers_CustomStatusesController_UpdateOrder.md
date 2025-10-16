[web] PUT /api/custom-statuses/{id:Guid}/reorder  (Workpapers.Next.API.Controllers.CustomStatusesController.UpdateOrder)  [L141–L147] status=200
  └─ sends_request ReorderCustomStatusCommand -> ReorderCustomStatusCommandandler [L144]
    └─ handled_by Workpapers.Next.Data.Commands.Dataverse.ReorderCustomStatusCommandandler.Handle [L35–L88]
      └─ uses_service DataverseService
        └─ method Put [L67]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.Put [L17-L127]
            └─ uses_service TenantIdentificationService
              └─ method GetStandardQueryParams [L70]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetStandardQueryParams [L15-L131]
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
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L65]
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
                      └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<CustomStatus> (AddScoped)
        └─ method ReadQuery [L58]
          └─ implementation Workpapers.Next.Data.Repository.CustomStatusRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ ReorderCustomStatusCommand
    └─ handlers 1
      └─ ReorderCustomStatusCommandandler
    └─ caches 1
      └─ IMemoryCache

