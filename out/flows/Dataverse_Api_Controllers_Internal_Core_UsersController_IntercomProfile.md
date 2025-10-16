[web] GET /api/internal/users/intercom-profile  (Dataverse.Api.Controllers.Internal.Core.UsersController.IntercomProfile)  [L111–L139] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IntercomService
    └─ method GetContactByExternalId [L119]
      └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.GetContactByExternalId [L17-L124]
  └─ uses_service UserService
    └─ method GetUserAsync [L114]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

