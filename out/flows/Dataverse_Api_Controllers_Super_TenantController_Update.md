[web] PUT /api/super/tenants/{id}  (Dataverse.Api.Controllers.Super.TenantController.Update)  [L190–L198] [auth=Authentication.MasterPolicy]
  └─ calls TenantRepository.WriteTable [L193]
  └─ writes_to Tenant [L193]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L193]

