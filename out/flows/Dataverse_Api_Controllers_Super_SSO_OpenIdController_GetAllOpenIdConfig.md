[web] GET /api/super/tenants/{tenantId}/sso/open-id/configs/  (Dataverse.Api.Controllers.Super.SSO.OpenIdController.GetAllOpenIdConfig)  [L31–L36] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IOpenIdService (AddScoped)
    └─ method GetAllOpenIdConfigAsync [L34]
      └─ implementation Dataverse.Services.Features.SSO.OpenIdService.GetAllOpenIdConfigAsync [L9-L53]
        └─ uses_service IdentityService
          └─ method AddOpenIdConfigAsync [L23]
            └─ implementation Dataverse.Services.Features.Identity.IdentityService.AddOpenIdConfigAsync [L14-L67]
              └─ uses_service EnvironmentConfig
                └─ method GetStandardQuery [L53]
                  └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ IOpenIdService

