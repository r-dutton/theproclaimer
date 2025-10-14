[web] GET /api/claims/{identityId}  (Dataverse.Api.Controllers.Tenants.ClaimsController.GetClaims)  [L54–L113] [auth=Authentication.MachineToMachinePolicy]
  └─ calls TenantRepository.ReadTable [L57]
  └─ queries Tenant [L57]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L106]
  └─ uses_service ITenantIdentificationService (AddScoped)
    └─ method AssignActiveTenant [L68]
  └─ uses_service TenantRepository
    └─ method ReadTable [L57]
  └─ sends_request GetTenantsForUserQuery [L64]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Tenants.Queries.GetTenantsForUserQueryHandler.Handle [L31–L55]
      └─ calls TenantRepository.ReadTable [L47]
      └─ calls TenantRepository.ReadTable [L44]
      └─ maps_to TenantWithUserInfoDto [L47]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L51]

