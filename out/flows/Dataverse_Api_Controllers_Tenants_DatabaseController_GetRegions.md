[web] GET /api/tenant/databases/regions  (Dataverse.Api.Controllers.Tenants.DatabaseController.GetRegions)  [L54–L68] [auth=master]
  └─ calls TenantRepository.ReadTable [L57]
  └─ queries Tenant [L57]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L57]
  └─ uses_service UserService
    └─ method GetIdentityId [L61]

