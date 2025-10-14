[web] GET /api/ui/support-users/available-users  (Dataverse.Api.Controllers.UI.Core.SupportUsersController.GetAvailableSupportUsersAsync)  [L46–L57] [auth=Authentication.AdminPolicy]
  └─ calls TenantRepository.ReadTable [L51]
  └─ queries Tenant [L51]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L51]

