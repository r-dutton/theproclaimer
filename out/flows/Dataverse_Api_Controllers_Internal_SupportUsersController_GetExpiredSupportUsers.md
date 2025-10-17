[web] GET /api/internal/support-users/expired-users  (Dataverse.Api.Controllers.Internal.SupportUsersController.GetExpiredSupportUsers)  [L46–L64] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TenantRepository.ReadTable [L50]
  └─ query Tenant [L50]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L50]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 1
      └─ TenantRepository

