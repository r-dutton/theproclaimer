[web] GET /api/super/tenants/{tenantId}/sso/open-id/configs/{configId}  (Dataverse.Api.Controllers.Super.SSO.OpenIdController.GetOpenIdConfigById)  [L38–L43] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IOpenIdService (AddScoped)
    └─ method GetOpenIdConfigByIdAsync [L41]
      └─ implementation Dataverse.Services.Features.SSO.OpenIdService.GetOpenIdConfigByIdAsync [L9-L53]

