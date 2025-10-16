[web] GET /api/super/support-users  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.GetSupportUsersAsync)  [L26–L34] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetSupportUsersQuery [L32]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetSupportUsersQueryHandler.Handle [L29–L61]
      └─ calls TenantRepository.ReadTable [L49]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L47]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]

