[web] PUT /api/dataverse/offices  (Workpapers.Next.API.Controllers.DataverseController.UpdateOfficeChanges)  [L104–L110] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L108]
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
  └─ sends_request ProcessDataverseOfficeCommand -> ProcessDataverseOfficeCommandHandler [L109]
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseOfficeCommandHandler.Handle [L23–L64]
      └─ uses_service IControlledRepository<Office> (Scoped (inferred))
        └─ method WriteQuery [L37]
          └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.WriteQuery
  └─ impact_summary
    └─ services 1
      └─ TenantService
    └─ requests 1
      └─ ProcessDataverseOfficeCommand
    └─ handlers 1
      └─ ProcessDataverseOfficeCommandHandler
    └─ caches 1
      └─ IMemoryCache

