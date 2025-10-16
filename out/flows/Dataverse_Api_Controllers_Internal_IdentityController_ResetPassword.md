[web] POST /api/internal/identity/{id}/reset-password  (Dataverse.Api.Controllers.Internal.IdentityController.ResetPassword)  [L119–L126] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IdentityService
    └─ method Post [L123]
      └─ implementation Dataverse.Services.Features.Identity.IdentityService.Post [L14-L67]
        └─ uses_service EnvironmentConfig
          └─ method GetStandardQuery [L53]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ IdentityService

