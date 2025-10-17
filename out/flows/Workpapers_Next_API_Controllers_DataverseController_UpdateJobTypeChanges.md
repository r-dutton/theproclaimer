[web] PUT /api/dataverse/job-types  (Workpapers.Next.API.Controllers.DataverseController.UpdateJobTypeChanges)  [L137–L143] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L141]
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
  └─ sends_request ProcessDataverseJobTypeCommand -> ProcessDataverseJobTypeCommandHandler [L142]
    └─ handled_by Workpapers.Next.Data.Commands.Dataverse.ProcessDataverseJobTypeCommandHandler.Handle [L21–L57]
      └─ calls JobTypeRepository (methods: Add,WriteQuery) [L46]
  └─ impact_summary
    └─ services 1
      └─ TenantService
    └─ requests 1
      └─ ProcessDataverseJobTypeCommand
    └─ handlers 1
      └─ ProcessDataverseJobTypeCommandHandler
    └─ caches 1
      └─ IMemoryCache

