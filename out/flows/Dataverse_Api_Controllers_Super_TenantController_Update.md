[web] PUT /api/super/tenants/{id}  (Dataverse.Api.Controllers.Super.TenantController.Update)  [L190–L198] status=200 [auth=Authentication.MasterPolicy]
  └─ calls TenantRepository.WriteTable [L193]
  └─ write Tenant [L193]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L193]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.WriteTable [L15-L180]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Tenant writes=1 reads=0
    └─ services 1
      └─ TenantRepository

