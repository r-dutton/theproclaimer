[web] GET /api/tenant/databases/regions  (Dataverse.Api.Controllers.Tenants.DatabaseController.GetRegions)  [L54–L68] status=200 [auth=master]
  └─ calls TenantRepository.ReadTable [L57]
  └─ query Tenant [L57]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L57]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ uses_service UserService
    └─ method GetIdentityId [L61]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetIdentityId [L15-L185]

