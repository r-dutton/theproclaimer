[web] POST /api/super/tenants/{tenantId}/sso/open-id/configs/  (Dataverse.Api.Controllers.Super.SSO.OpenIdController.AddNewOpenIdConfig)  [L24–L29] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IOpenIdService (AddScoped)
    └─ method AddOpenIdConfigAsync [L27]
      └─ implementation Dataverse.Services.Features.SSO.OpenIdService.AddOpenIdConfigAsync [L9-L53]

