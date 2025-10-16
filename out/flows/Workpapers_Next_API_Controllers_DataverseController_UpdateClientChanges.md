[web] PUT /api/dataverse/clients  (Workpapers.Next.API.Controllers.DataverseController.UpdateClientChanges)  [L115–L121] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L119]
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
  └─ sends_request ProcessDataverseClientCommand -> ProcessDataverseClientCommandHandler [L120]
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseClientCommandHandler.Handle [L38–L251]
      └─ calls FileRepository.WriteQueryWithArchived [L106]
      └─ maps_to DataverseEntityMap [L246]
        └─ automapper.registration DataverseMappingProfile (User->DataverseEntityMap) [L82]
      └─ uses_service IControlledRepository<User> (Scoped (inferred))
        └─ method ReadQuery [L235]
          └─ implementation Cirrus.Data.Repository.Firm.UserRepository.ReadQuery
      └─ uses_service IControlledRepository<Team> (Scoped (inferred))
        └─ method ReadQuery [L223]
          └─ implementation Cirrus.Data.Repository.Firm.TeamRepository.ReadQuery
      └─ uses_service FirmSettingsService
        └─ method GetFirmJurisdictions [L189]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetFirmJurisdictions [L15-L112]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method GetCurrentSettings [L45]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCurrentSettings [L7-L35]
            └─ uses_service TenantService
              └─ method GetCurrentSettings [L34]
                └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentSettings [L26-L139]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method GetCatalogByDataverseId [L111]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
                  └─ uses_service Tenant
                    └─ method AssignCurrentTenantId [L80]
                      └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
            └─ uses_cache IDistributedCache.SetStringAsync [write] [L108]
            └─ uses_cache IDistributedCache.GetStringAsync [read] [L98]
      └─ uses_service IControlledRepository<Office> (Scoped (inferred))
        └─ method ReadQuery [L178]
          └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
      └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
        └─ method ReadQuery [L114]
          └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
      └─ uses_service IControlledRepository<Client> (Scoped (inferred))
        └─ method WriteQuery [L81]
          └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.WriteQuery
  └─ impact_summary
    └─ services 1
      └─ TenantService
    └─ requests 1
      └─ ProcessDataverseClientCommand
    └─ handlers 1
      └─ ProcessDataverseClientCommandHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ DataverseEntityMap

