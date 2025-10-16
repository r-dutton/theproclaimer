[web] POST /api/super/tenants/{tenantId}/sso/open-id/configs/  (Dataverse.Api.Controllers.Super.SSO.OpenIdController.AddNewOpenIdConfig)  [L24–L29] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IOpenIdService (AddScoped)
    └─ method AddOpenIdConfigAsync [L27]
      └─ implementation Dataverse.Services.Features.SSO.OpenIdService.AddOpenIdConfigAsync [L9-L53]
        └─ uses_service IdentityService
          └─ method AddOpenIdConfigAsync [L23]
            └─ implementation Dataverse.Services.Features.Identity.IdentityService.AddOpenIdConfigAsync [L14-L67]
              └─ uses_service EnvironmentConfig
                └─ method GetStandardQuery [L53]
                  └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ IOpenIdService

