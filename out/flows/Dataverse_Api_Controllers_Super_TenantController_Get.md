[web] GET /api/super/tenants/{id}  (Dataverse.Api.Controllers.Super.TenantController.Get)  [L90–L110] [auth=Authentication.MasterPolicy]
  └─ maps_to TenantDto [L93]
  └─ calls TenantRepository.ReadTable [L93]
  └─ queries Tenant [L93]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L93]
  └─ uses_service UserService
    └─ method GetIdentityId [L103]

