[web] PUT /api/dataverse/users  (Workpapers.Next.API.Controllers.DataverseController.UpdateUserChanges)  [L92–L99] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L97]
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
  └─ sends_request ProcessDataverseUserCommand -> ProcessDataverseUserCommandHandler [L98]
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseUserCommandHandler.Handle [L28–L89]
      └─ maps_to DataverseEntityMap [L62]
        └─ automapper.registration DataverseMappingProfile (Team->DataverseEntityMap) [L81]
      └─ uses_service IControlledRepository<Team> (Scoped (inferred))
        └─ method ReadQuery [L62]
          └─ implementation Cirrus.Data.Repository.Firm.TeamRepository.ReadQuery
      └─ uses_service IControlledRepository<Office> (Scoped (inferred))
        └─ method ReadQuery [L56]
          └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
      └─ uses_service IControlledRepository<User> (Scoped (inferred))
        └─ method WriteQuery [L46]
          └─ implementation Cirrus.Data.Repository.Firm.UserRepository.WriteQuery
  └─ impact_summary
    └─ services 1
      └─ TenantService
    └─ requests 1
      └─ ProcessDataverseUserCommand
    └─ handlers 1
      └─ ProcessDataverseUserCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ DataverseEntityMap

