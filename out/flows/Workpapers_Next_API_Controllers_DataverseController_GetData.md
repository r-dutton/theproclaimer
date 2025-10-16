[web] GET /api/dataverse/{entityRoute}/audit  (Workpapers.Next.API.Controllers.DataverseController.GetData)  [L367–L379] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L374]
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
  └─ sends_request DataverseAuditQuery -> DataverseAuditQueryHandler [L376]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.DataverseAuditQueryHandler.Handle [L26–L69]
      └─ maps_to DataverseAuditDto [L63]
        └─ automapper.registration CirrusMappingProfile (Entity->DataverseAuditDto) [L178]
      └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
        └─ method ReadQuery [L63]
          └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
      └─ uses_service IControlledRepository<Client> (Scoped (inferred))
        └─ method ReadQuery [L62]
          └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.ReadQuery
      └─ uses_service IControlledRepository<User> (Scoped (inferred))
        └─ method ReadQuery [L60]
          └─ implementation Cirrus.Data.Repository.Firm.UserRepository.ReadQuery
      └─ uses_service IControlledRepository<Team> (Scoped (inferred))
        └─ method ReadQuery [L58]
          └─ implementation Cirrus.Data.Repository.Firm.TeamRepository.ReadQuery
      └─ uses_service IControlledRepository<Office> (Scoped (inferred))
        └─ method ReadQuery [L56]
          └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
  └─ impact_summary
    └─ services 1
      └─ TenantService
    └─ requests 1
      └─ DataverseAuditQuery
    └─ handlers 1
      └─ DataverseAuditQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ DataverseAuditDto

