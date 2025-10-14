[web] GET /api/users/intercom-profile  (Workpapers.Next.API.Controllers.UsersController.GetIntercomProfile)  [L149–L155] [auth=AuthorizationPolicies.User]
  └─ sends_request GetIntercomProfileQuery [L153]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Dataverse.Queries.GetIntercomProfileQueryHandler.Handle [L24–L61]
      └─ uses_service DataverseService
        └─ method Get [L52]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L39]
      └─ uses_service UserService
        └─ method GetUser [L42]
  └─ returns IntercomProfileDto [L153]

