[web] DELETE /api/super/tenants/{id}  (Dataverse.Api.Controllers.Super.TenantController.Delete)  [L200–L208] status=200 [auth=Authentication.MasterPolicy]
  └─ calls TenantRepository.WriteTable [L203]
  └─ write Tenant [L203]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L203]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.WriteTable [L15-L180]

