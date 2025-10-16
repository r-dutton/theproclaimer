[web] GET /api/claims/{identityId}  (Dataverse.Api.Controllers.Tenants.ClaimsController.GetClaims)  [L54–L113] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ calls TenantRepository.ReadTable [L57]
  └─ query Tenant [L57]
    └─ reads_from Tenants
  └─ uses_service ITenantIdentificationService (AddScoped)
    └─ method AssignActiveTenant [L68]
      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.AssignActiveTenant [L27-L149]
        └─ uses_service TenantDetails
          └─ method AssignActiveTenant [L77]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ uses_service TenantRepository
    └─ method ReadTable [L57]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ sends_request GetTenantsForUserQuery -> GetTenantsForUserQueryHandler [L64]
    └─ handled_by Dataverse.Tenants.Queries.GetTenantsForUserQueryHandler.Handle [L31–L55]
      └─ calls TenantRepository.ReadTable [L47]
      └─ maps_to TenantWithUserInfoDto [L47]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 2
      └─ ITenantIdentificationService
      └─ TenantRepository
    └─ requests 1
      └─ GetTenantsForUserQuery
    └─ handlers 1
      └─ GetTenantsForUserQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ TenantWithUserInfoDto

