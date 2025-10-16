[web] GET /api/users/intercom-profile  (Workpapers.Next.API.Controllers.UsersController.GetIntercomProfile)  [L149–L155] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetIntercomProfileQuery [L153]
    └─ handled_by Cirrus.Central.Dataverse.Queries.GetIntercomProfileQueryHandler.Handle [L24–L61]
      └─ uses_service UserService
        └─ method GetUser [L42]
          └─ implementation Cirrus.ApplicationService.Services.UserService.GetUser [L14-L122]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L39]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
      └─ uses_service DataverseService
        └─ method Get [L52]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]
  └─ returns IntercomProfileDto [L153]

