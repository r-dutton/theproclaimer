[web] PUT /api/dataverse/custom-statuses  (Workpapers.Next.API.Controllers.DataverseController.UpdateCustomStatusChanges)  [L126–L132] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L130]
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
  └─ sends_request ProcessDataverseCustomStatusCommand -> ProcessDataverseCustomStatusCommandHandler [L131]
    └─ handled_by Workpapers.Next.Data.Commands.Dataverse.ProcessDataverseCustomStatusCommandHandler.Handle [L21–L57]
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method WriteQuery [L34]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ TenantService
    └─ requests 1
      └─ ProcessDataverseCustomStatusCommand
    └─ handlers 1
      └─ ProcessDataverseCustomStatusCommandHandler
    └─ caches 1
      └─ IMemoryCache

