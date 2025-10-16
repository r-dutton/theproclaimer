[web] GET /api/ui/support-users/available-users  (Dataverse.Api.Controllers.UI.Core.SupportUsersController.GetAvailableSupportUsersAsync)  [L46–L57] status=200 [auth=Authentication.AdminPolicy]
  └─ calls TenantRepository.ReadTable [L51]
  └─ query Tenant [L51]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L51]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 1
      └─ TenantRepository

