[web] GET /api/internal/tenants/{id}  (Dataverse.Api.Controllers.Internal.TenantController.Get)  [L53–L60] [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to TenantDto [L56]
  └─ calls TenantRepository.ReadTable [L56]
  └─ queries Tenant [L56]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L56]

