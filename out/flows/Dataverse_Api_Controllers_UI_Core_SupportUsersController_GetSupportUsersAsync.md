[web] GET /api/ui/support-users  (Dataverse.Api.Controllers.UI.Core.SupportUsersController.GetSupportUsersAsync)  [L36–L44] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request GetSupportUsersQuery [L42]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetSupportUsersQueryHandler.Handle [L29–L61]
      └─ calls TenantRepository.ReadTable [L49]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L47]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]

