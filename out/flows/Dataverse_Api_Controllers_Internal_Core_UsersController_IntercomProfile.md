[web] GET /api/internal/users/intercom-profile  (Dataverse.Api.Controllers.Internal.Core.UsersController.IntercomProfile)  [L111–L139] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IntercomService
    └─ method GetContactByExternalId [L119]
  └─ uses_service UserService
    └─ method GetUserAsync [L114]

