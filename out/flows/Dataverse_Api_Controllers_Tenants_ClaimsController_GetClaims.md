[web] GET /api/claims/{identityId}  (Dataverse.Api.Controllers.Tenants.ClaimsController.GetClaims)  [L54–L113] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ calls TenantRepository.ReadTable [L57]
  └─ query Tenant [L57]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L106]
      └─ ... (no implementation details available)
  └─ uses_service ITenantIdentificationService (AddScoped)
    └─ method AssignActiveTenant [L68]
      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.AssignActiveTenant [L27-L149]
  └─ uses_service TenantRepository
    └─ method ReadTable [L57]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ sends_request GetTenantsForUserQuery [L64]
    └─ handled_by Dataverse.Tenants.Queries.GetTenantsForUserQueryHandler.Handle [L31–L55]
      └─ calls TenantRepository.ReadTable [L47]
      └─ calls TenantRepository.ReadTable [L44]
      └─ maps_to TenantWithUserInfoDto [L47]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L51]
          └─ ... (no implementation details available)

