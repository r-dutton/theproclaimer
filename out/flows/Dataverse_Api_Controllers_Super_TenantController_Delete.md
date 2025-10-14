[web] DELETE /api/super/tenants/{id}  (Dataverse.Api.Controllers.Super.TenantController.Delete)  [L200–L208] [auth=Authentication.MasterPolicy]
  └─ calls TenantRepository.WriteTable [L203]
  └─ writes_to Tenant [L203]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L203]

